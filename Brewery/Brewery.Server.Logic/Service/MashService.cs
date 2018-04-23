using Brewery.Core.Contracts.ServiceAdapter;
using Brewery.Core.Models;
using Brewery.Server.Core.Models;
using System;

namespace Brewery.Server.Logic.Service
{
    class MashService : Core.Service.IMashService
    {
        private MashServiceStatus _mashServiceStatus;
        private readonly IBoilingPlate1Service _boilingPlate1Service;
        private readonly IPiezoService _piezoService;
        private readonly IMixerService _mixerService;
        private readonly MashSteps _brewProcessSteps;
        private DateTime _tempReachedAt = default(DateTime);
        private int _currentStep = 0;
        private bool _messageOpen;
        private bool _messageAcknowledged;
        private DateTime _startedAt = default(DateTime);

        public MashService(IBoilingPlate1Service boilingPlate1Service, IPiezoService piezoService, IMixerService mixerService, MashSteps mashSteps, MashServiceStatus mashServiceStatus)
        {
            _boilingPlate1Service = boilingPlate1Service;
            _piezoService = piezoService;
            _mixerService = mixerService;
            _brewProcessSteps = mashSteps;
            _mashServiceStatus = mashServiceStatus;
        }

        public MashServiceStatus GetStatus()
        {
            return _mashServiceStatus;
        }

        public void StopMashProcess()
        {
            _mashServiceStatus.Status = ServiceStatus.Stopped;
            _boilingPlate1Service.PowerOff();
            _tempReachedAt = default(DateTime);
            _startedAt = default(DateTime);
            _currentStep = 0;
        }

        public void PauseMashProcess()
        {
            _mashServiceStatus.Status = ServiceStatus.Paused;
        }

        public void StartMashProcess()
        {
            if (_mashServiceStatus.Status != ServiceStatus.Paused)
            {
                foreach (var brewProcessStep in _brewProcessSteps)
                {
                    brewProcessStep.ElapsedTime = null;
                }
            }
            _mashServiceStatus.Status = ServiceStatus.Started;
        }

        public void MessageAcknowledged()
        {
            _messageOpen = false;
            _messageAcknowledged = true;
            _mashServiceStatus.Message = null;
        }

        public async void Execute()
        {
            await _piezoService.Power(false);

            if (_mashServiceStatus.Status != ServiceStatus.Started)
                return;

            var currentStep = _brewProcessSteps[_currentStep];
            if (!currentStep.Active)
            {
                SetNextStep();
                return;
            }

            if (_startedAt == default(DateTime))
                _startedAt = DateTime.Now;

            var elapsed = (DateTime.Now - _startedAt);

            currentStep.ElapsedTime = $"{elapsed.Hours.ToString("00")}:{elapsed.Minutes.ToString("00")}:{elapsed.Seconds.ToString("00")}";

            await _boilingPlate1Service.ManageTemperature(currentStep.Temperature);
            await _mixerService.Power(currentStep.Mixer);
            var temperature1 = await _boilingPlate1Service.GetCurrenTemperature();

            //wenn ein nachfolgender Schritt eine niedrigere Temperatur benötigt als der Vorgängerschritt
            if (_currentStep > 0 && currentStep.Temperature < _brewProcessSteps[_currentStep - 1].Temperature && _tempReachedAt == default(DateTime))
            {
                if (temperature1 <= currentStep.Temperature)
                {
                    _tempReachedAt = DateTime.Now;
                }
            }
            //wenn Solltemperatur erreicht
            else if (temperature1 >= currentStep.Temperature)
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

                            _mashServiceStatus.Message = currentStep.ToString();
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
    }
}