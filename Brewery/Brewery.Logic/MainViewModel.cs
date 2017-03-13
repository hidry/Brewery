using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Brewery.Core.Contracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Brewery.Logic
{
    public class MainViewModel : ViewModelBase
    {
        private const double TemperatureSteps = 1.0;
        private readonly IMixerModule _mixerModule;

        private DateTime _dateTime;
        public DateTime DateTime { get { return _dateTime; } private set { Set(() => DateTime, ref _dateTime, value); } }

        private double _temperatureCurrent;
        public double TemperatureCurrent
        {
            get { return _temperatureCurrent; }
            private set
            {
                Set(() => TemperatureCurrent, ref _temperatureCurrent, value);
            }
        }

        private double _temperatureConfigured;
        public double TemperatureConfigured
        {
            get { return _temperatureConfigured; }
            private set
            {
                Set(() => TemperatureConfigured, ref _temperatureConfigured, value);
            }
        }

        private bool _mixerStatus;
        public bool MixerStatus
        {
            get { return _mixerStatus; }
            private set { Set(() => MixerStatus, ref _mixerStatus, value); }
        }

        private bool _temperatureControl;
        public bool TemperatureControl
        {
            get { return _temperatureControl; }
            private set
            {
                Set(() => TemperatureControl, ref _temperatureControl, value);
            }
        }

        private bool _boilingPlate;
        public bool BoilingPlate
        {
            get { return _boilingPlate; }
            private set
            {
                Set(() => BoilingPlate, ref _boilingPlate, value);
            }
        }

        public MainViewModel(IDateTimeModule dateTimeModule, IMixerModule mixerModule, ITemperatureModule temperatureModule, ITemperatureControlModule temperatureControlModule)
        {
            _mixerModule = mixerModule;
            TemperatureConfigured = 50.0;
            TemperatureControl = false;
            MixerStatus = false;
            var everySecondExecutedActions = new List<Action>()
            {
                () => TemperatureCurrent = temperatureModule.GetCurrenTemperature().Temperature,
                () => DateTime = dateTimeModule.GetCurrentDateTime().DateTime,
                () => BoilingPlate = temperatureControlModule.ControlTemperature(TemperatureControl, TemperatureConfigured, TemperatureCurrent).Heating
            };
            var timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            timer.Tick += (sender, o) => everySecondExecutedActions.ForEach(a => a.Invoke());
            timer.Start();
        }

        private void ToggleMixer()
        {
            MixerStatus = _mixerModule.ToggleStatus().Status;
        }

        public RelayCommand ToggleMixerCommand => new RelayCommand(ToggleMixer);

        public RelayCommand TemperatureDownCommand => new RelayCommand(TemperatureDown);

        private void TemperatureDown()
        {
            TemperatureConfigured -= TemperatureSteps;
        }

        public RelayCommand TemperatureUpCommand => new RelayCommand(TemperatureUp);

        private void TemperatureUp()
        {
            TemperatureConfigured += TemperatureSteps;
        }

        public RelayCommand ToggleTemperatureControlCommand => new RelayCommand(ToggleTemperatureControl);

        private void ToggleTemperatureControl()
        {
            TemperatureControl = !TemperatureControl;
        }
    }
}