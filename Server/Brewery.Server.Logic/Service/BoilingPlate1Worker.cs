using Brewery.Core.Contracts.ServiceAdapter;
using Brewery.Server.Core.Api;
using Brewery.Server.Core.Models;
using Brewery.Server.Logic.Api.Hub;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Brewery.Server.Logic.Service
{
    public class BoilingPlate1Worker : Core.Service.IBoilingPlate1Worker
    {
        enum ServiceStatus
        {
            Started,
            Paused,
            Stopped,
            Finished
        }

        private ServiceStatus _serviceStatus = ServiceStatus.Stopped;
        private readonly IBoilingPlate1Service _boilingPlate1Service;
        private readonly IPiezoService _piezoService;
        private readonly IMixerService _mixerService;
        private readonly MashSteps _brewProcessSteps;
        private DateTime _tempReachedAt = default(DateTime);
        private int _currentStep = 0;
        private bool _messageOpen;
        private bool _messageAcknowledged;
        private DateTime _startedAt = default(DateTime);

        public IGpioModule _gpioModule { get; }

        public BoilingPlate1Worker(IGpioModule gpioModule, IBoilingPlate1Service boilingPlate1Service, IPiezoService piezoService, IMixerService mixerService, MashSteps mashSteps)
        {
            _gpioModule = gpioModule;
            _boilingPlate1Service = boilingPlate1Service;
            _piezoService = piezoService;
            _mixerService = mixerService;
            _brewProcessSteps = mashSteps;
            _piezoService.Power(false);
        }

        public bool GetPowerStatus()
        {
            if (_serviceStatus == ServiceStatus.Stopped)
                return false;
            return true;
        }

        public void StopMashProcess()
        {
            _serviceStatus = ServiceStatus.Stopped;
            Power(false);
            _tempReachedAt = default(DateTime);
            _startedAt = default(DateTime);
            _currentStep = 0;
        }

        private void Power(bool on)
        {
            _gpioModule.Power(Settings.BoilingPlate1Gpio.GpioNumber, on);
        }

        public void PauseMashProcess()
        {
            _serviceStatus = ServiceStatus.Paused;
        }

        public void StartMashProcess()
        {
            if (_serviceStatus != ServiceStatus.Paused)
            {
                foreach (var brewProcessStep in _brewProcessSteps)
                {
                    brewProcessStep.Elapsed = default(TimeSpan);
                }
            }
            _serviceStatus = ServiceStatus.Started;
        }

        public void AcknowledgeMessage()
        {
            _messageOpen = false;
            _messageAcknowledged = true;
        }

        private void ManageTemperature(double temperatureCurrent, double temperature)
        {
            if (temperatureCurrent < temperature)
            {
                Power(true);
            }
            else
            {
                Power(false);
            }
        }

        public async Task Execute()
        {
            try
            {
                await _piezoService.Power(false);

                if (_serviceStatus != ServiceStatus.Started)
                {
                    Power(false);
                    return;
                }

                var currentStep = GetCurrentStep();
                
                if (_startedAt == default(DateTime))
                    _startedAt = DateTime.Now;

                var elapsed = (DateTime.Now - _startedAt);
                currentStep.Elapsed = elapsed;

                var currentTemperature = await _boilingPlate1Service.GetCurrenTemperature();
                ManageTemperature(currentTemperature, currentStep.Temperature);
                await _mixerService.Power(currentStep.Mixer);

                CalculateRemainingTime(currentStep, currentTemperature);

                //wenn ein nachfolgender Schritt eine niedrigere Temperatur benötigt als der Vorgängerschritt
                if (_currentStep > 0 && currentStep.Temperature < _brewProcessSteps[_currentStep - 1].Temperature && _tempReachedAt == default(DateTime))
                {
                    if (currentTemperature <= currentStep.Temperature)
                    {
                        _tempReachedAt = DateTime.Now;
                    }
                }
                //wenn Solltemperatur erreicht
                else if (currentTemperature >= currentStep.Temperature)
                {
                    if (_tempReachedAt == default(DateTime))
                        _tempReachedAt = DateTime.Now;

                    //Rast
                    if (_tempReachedAt.AddMinutes(currentStep.Rast) <= DateTime.Now)
                    {
                        //Evtl. Meldung anzeigen und warten bis bestätigt
                        if (currentStep.Alert && !_messageAcknowledged)
                        {
                            if (!_messageOpen)
                            {
                                _messageOpen = true;

                                try
                                {
                                    //SendBrewStepNotification(currentStep); // wenn kein Netzwerk verfügbar Exception!?
                                }
                                catch (Exception)
                                {
                                    //todo: logging
                                }
                            }
                            await _piezoService.Power(true);
                        }
                        else
                        {
                            SetNextStep();
                        }
                    }
                }

                // Broadcast updates via SignalR
                var hubContext = HubContextProvider.BoilingPlate1HubContext;
                if (hubContext != null)
                {
                    await BoilingPlate1Hub.BroadcastPowerStatus(hubContext, GetPowerStatus());
                    await BoilingPlate1Hub.BroadcastCurrentTemperature(hubContext, currentTemperature);
                    await BoilingPlate1Hub.BroadcastCurrentStep(hubContext, currentStep.Step, currentStep.EstimatedTime);

                    // Also broadcast mash steps updates
                    var mashStepsHubContext = HubContextProvider.MashStepsHubContext;
                    if (mashStepsHubContext != null)
                    {
                        await MashStepsHub.BroadcastCurrentStep(mashStepsHubContext, currentStep);
                        await MashStepsHub.BroadcastTotalEstimatedRemainingTime(mashStepsHubContext, _brewProcessSteps.Sum(ms => ms.EstimatedTime));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void CalculateRemainingTime(MashStep currentStep, double currentTemperature)
        {
            var tempReached = true;
            if (_tempReachedAt == default(DateTime))
                tempReached = false;
            CalculateRamainingTime(currentStep, currentTemperature, tempReached);
            for (int i = _currentStep + 1; i < _brewProcessSteps.Count; i++)
            {
                CalculateRamainingTime(_brewProcessSteps[i], _brewProcessSteps[i - 1].Temperature, false);
            }
        }

        private void CalculateRamainingTime(MashStep currentStep, double startTemperature, bool tempReached)
        {            
            TimeSpan estimatedTime;
            if (!tempReached)
            {
                var kw = 7;
                var liter = 70;
                var wirkungsgrad = 0.8;
                var waermekapazitaetWasser = 4.1897;
                var anzahlSekundenProMinute = 60;

                if (currentStep.Temperature <= startTemperature)
                {
                    estimatedTime = TimeSpan.FromMinutes(currentStep.Rast);
                }
                else
                {
                    var calculatedTimeToReachTempareture = ((liter * waermekapazitaetWasser * (currentStep.Temperature - startTemperature)) / (wirkungsgrad * kw)) / anzahlSekundenProMinute;
                    estimatedTime = TimeSpan.FromMinutes(currentStep.Rast + calculatedTimeToReachTempareture);
                }
            }
            else
            {
                estimatedTime = TimeSpan.FromMinutes(currentStep.Rast - currentStep.Elapsed.Minutes);
            }
            currentStep.EstimatedTime = (int)Math.Round(estimatedTime.TotalMinutes);
        }

        private void SetNextStep()
        {
            if (_brewProcessSteps.Count - 1 > _currentStep)
            {
                _currentStep += 1;
            }
            else
            {
                _currentStep = 0;
            }
            _tempReachedAt = default(DateTime);
            _startedAt = default(DateTime);
            _messageAcknowledged = false;
        }

        public MashStep GetCurrentStep()
        {
            return _brewProcessSteps[_currentStep];
        }
    }
}