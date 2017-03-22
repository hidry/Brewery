﻿using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Brewery.Core.Contracts;
using Brewery.Core.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Brewery.Logic
{
    public class BrewProcessViewModel : ViewModelBase
    {
        private readonly IMixerModule _mixerModule;
        private readonly ITemperature1Module _temperature1Module;
        private readonly ITemperatureControl1Module _temperatureControl1Module;

        public BrewProcessViewModel(IMixerModule mixerModule, ITemperature1Module temperature1Module, ITemperatureControl1Module temperatureControl1Module)
        {
            _mixerModule = mixerModule;
            _temperature1Module = temperature1Module;
            _temperatureControl1Module = temperatureControl1Module;

            _brewProcessTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 10) };
            _brewProcessTimer.Tick += (sender, o) => { ExecuteBrewProcessStep(); };

            ButtonStartBrewProcessEnabled = true;
            ButtonPauseBrewProcessEnabled = false;
            ButtonStopBrewProcessEnabled = false;
            ButtonRemoveBrewProcessStepEnabled = false;

            BrewProcessSteps.Add(new BrewProcessStep()
            {
                Temperatur = 67,
                Rast = 0,
                Benachrichtigung = true,
                Ruehrgeraet = false
            });
            BrewProcessSteps.Add(new BrewProcessStep()
            {
                Temperatur = 76,
                Rast = 90,
                Benachrichtigung = true,
                Ruehrgeraet = true
            });
        }
        public ObservableCollection<BrewProcessStep> BrewProcessSteps = new ObservableCollection<BrewProcessStep>();

        public RelayCommand AddBrewProcessStepCommand => new RelayCommand(AddBrewProcessStep);

        private void AddBrewProcessStep()
        {
            BrewProcessSteps.Add(new BrewProcessStep());
        }

        private BrewProcessStep _selectedBrewProcessStep;
        public BrewProcessStep SelectedBrewProcessStep
        {
            get { return _selectedBrewProcessStep; }
            set {
                Set(() => SelectedBrewProcessStep, ref _selectedBrewProcessStep, value);
                ButtonRemoveBrewProcessStepEnabled = value != null;
            }
        }

        private bool _buttonStartBrewProcessEnabled;
        public bool ButtonStartBrewProcessEnabled
        {
            get { return _buttonStartBrewProcessEnabled; }
            set { Set(() => ButtonStartBrewProcessEnabled, ref _buttonStartBrewProcessEnabled, value); }
        }

        private bool _buttonPauseBrewProcessEnabled;
        public bool ButtonPauseBrewProcessEnabled
        {
            get { return _buttonPauseBrewProcessEnabled; }
            set { Set(() => ButtonPauseBrewProcessEnabled, ref _buttonPauseBrewProcessEnabled, value); }
        }

        private bool _buttonStopBrewProcessEnabled;
        public bool ButtonStopBrewProcessEnabled
        {
            get { return _buttonStopBrewProcessEnabled; }
            set { Set(() => ButtonStopBrewProcessEnabled, ref _buttonStopBrewProcessEnabled, value); }
        }

        private bool _buttonRemoveBrewProcessStepEnabled;
        public bool ButtonRemoveBrewProcessStepEnabled
        {
            get { return _buttonRemoveBrewProcessStepEnabled; }
            set { Set(() => ButtonRemoveBrewProcessStepEnabled, ref _buttonRemoveBrewProcessStepEnabled, value ); }
        }

        public RelayCommand RemoveBrewProcessStepCommand => new RelayCommand(RemoveBrewProcessStep);

        private void RemoveBrewProcessStep()
        {
            BrewProcessSteps.Remove(SelectedBrewProcessStep);
            SelectedBrewProcessStep = null;
        }

        public RelayCommand StartBrewProcessCommand => new RelayCommand(StartBrewProcess);

        private void StartBrewProcess()
        {
            ButtonStartBrewProcessEnabled = false;
            ButtonPauseBrewProcessEnabled = true;
            ButtonStopBrewProcessEnabled = true;
            _brewProcessTimer.Start();
        }

        private readonly DispatcherTimer _brewProcessTimer;
        private DateTime _tempReachedAt = default(DateTime);
        private int _currentStep = 0;
        private bool _messageOpen;
        private bool _messageAcknowledged;

        private void ExecuteBrewProcessStep()
        {
            var temperatureCurrent = _temperature1Module.GetCurrenTemperature().Temperature;
            var currentStep = BrewProcessSteps[_currentStep];

            _temperatureControl1Module.ManageTemperature(currentStep.Temperatur, temperatureCurrent);
            
            _mixerModule.Power(currentStep.Ruehrgeraet);
            
            //wenn Solltemperatur erreicht
            if (temperatureCurrent >= currentStep.Temperatur)
            {
                if (_tempReachedAt == default(DateTime))
                    _tempReachedAt = DateTime.Now;

                //Rast
                if (_tempReachedAt.AddMinutes(currentStep.Rast) <= DateTime.Now)
                {
                    //Evtl. Meldung anzeigen und warten bis bestätigt
                    if (currentStep.Benachrichtigung && !_messageAcknowledged)
                    {
                        if (!_messageOpen)
                        {
                            _messageOpen = true;
                            Messenger.Default.Send(new ShowMessageDialog()
                            {
                                Title = "Rast-Ende",
                                Message = currentStep.ToString(),
                                AfterHideCallback = (confirmed) =>
                                {
                                    _messageOpen = false;
                                    _messageAcknowledged = true;
                                }
                            }); // todo: ressourcen
                        }
                    }
                    else
                    {
                        if (BrewProcessSteps.Count - 1 > _currentStep)
                        {
                            _currentStep += 1;
                        }
                        else
                        {
                            _brewProcessTimer.Stop();
                            _currentStep = 0;
                            ButtonStartBrewProcessEnabled = true;
                            ButtonPauseBrewProcessEnabled = false;
                            ButtonStopBrewProcessEnabled = false;
                        }
                        _tempReachedAt = default(DateTime);
                        _messageAcknowledged = false;
                    }
                }
            }
        }

        public RelayCommand PauseBrewProcessCommand => new RelayCommand(PauseBrewProcess);

        private void PauseBrewProcess()
        {
            ButtonPauseBrewProcessEnabled = false;
            ButtonStartBrewProcessEnabled = true;
            _brewProcessTimer.Stop();
        }

        public RelayCommand StopBrewProcessCommand => new RelayCommand(StopBrewProcess);

        private void StopBrewProcess()
        {
            ButtonStartBrewProcessEnabled = true;
            ButtonStopBrewProcessEnabled = false;
            ButtonPauseBrewProcessEnabled = false;
            _brewProcessTimer.Stop();
            _tempReachedAt = default(DateTime);
            _currentStep = 0;
        }
    }

    public class BrewProcessStep
    {
        //todo: prüfen ob Annotations verfügbar https://feedback.telerik.com/Project/167/Feedback/List/Your%20Items
        public double Temperatur { get; set; }
        public int Rast { get; set; }
        public bool Benachrichtigung { get; set; }
        public bool Ruehrgeraet { get; set; }
        public override string ToString()
        {
            return $"{nameof(Temperatur)}: {Temperatur}, {nameof(Rast)}: {Rast}, {nameof(Benachrichtigung)}: {Benachrichtigung}, {nameof(Ruehrgeraet)}: {Ruehrgeraet}";
        }
    }
}