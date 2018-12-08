using Brewery.Core.Contracts.ServiceAdapter;
using Brewery.Server.Core.Models;
using System;
using System.Threading.Tasks;

namespace Brewery.Server.Logic.Service
{
    class BoilingPlate1Worker : Core.Service.IBoilingPlate1Worker
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

        public BoilingPlate1Worker(IBoilingPlate1Service boilingPlate1Service, IPiezoService piezoService, IMixerService mixerService, MashSteps mashSteps)
        {
            _boilingPlate1Service = boilingPlate1Service;
            _piezoService = piezoService;
            _mixerService = mixerService;
            _brewProcessSteps = mashSteps;
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
            _boilingPlate1Service.PowerOff();
            _tempReachedAt = default(DateTime);
            _startedAt = default(DateTime);
            _currentStep = 0;
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

        public void MessageAcknowledged()
        {
            _messageOpen = false;
            _messageAcknowledged = true;
        }

        private async Task ManageTemperature(double temperatureCurrent, double temperature)
        {
            if (temperatureCurrent < temperature)
            {
                await _boilingPlate1Service.PowerOn();
            }
            else
            {
                await _boilingPlate1Service.PowerOff();
            }
        }

        public async Task Execute()
        {
            await _piezoService.Power(false);

            if (_serviceStatus != ServiceStatus.Started)
                return;

            var currentStep = GetCurrentStep();
            if (!currentStep.Active)
            {
                SetNextStep();
                return;
            }

            if (_startedAt == default(DateTime))
                _startedAt = DateTime.Now;

            var elapsed = (DateTime.Now - _startedAt);
            currentStep.Elapsed = elapsed;
            
            var currentTemperature = await _boilingPlate1Service.GetCurrenTemperature();
            await ManageTemperature(currentTemperature, currentStep.Temperature);
            await _mixerService.Power(currentStep.Mixer);

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