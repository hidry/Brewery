using System;
using Windows.UI.Xaml;
using Brewery.Core.Contracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml.Input;

namespace Brewery.Logic
{
    public class MainViewModel : ViewModelBase
    {
        private const double TemperatureSteps = 1.0;
        private readonly IDateTimeModule _dateTimeModule;
        private readonly IMixerModule _mixerModule;
        private readonly ITemperatureModule _temperatureModule;
        private readonly ITemperatureControlModule _temperatureControlModule;

        private DateTime _dateTime;
        public DateTime DateTime { get { return _dateTime; } private set { Set(() => DateTime, ref _dateTime, value); } }

        private double _temperatureCurrent;
        public double TemperatureCurrent
        {
            get { return _temperatureCurrent; }
            set
            {
                Set(() => TemperatureCurrent, ref _temperatureCurrent, value);
            }
        }

        private double _temperatureConfigured;
        public double TemperatureConfigured
        {
            get { return _temperatureConfigured; }
            set
            {
                Set(() => TemperatureConfigured, ref _temperatureConfigured, value);
            }
        }

        private bool _mixerStatus;
        public bool MixerStatus { get { return _mixerStatus; } set { Set(() => MixerStatus, ref _mixerStatus, value); } }

        private bool _temperatureControl;
        public bool TemperatureControl
        {
            get { return _temperatureControl; }
            set
            {
                Set(() => TemperatureControl, ref _temperatureControl, value);
            }
        }

        private bool _boilingPlate;
        public bool BoilingPlate
        {
            get { return _boilingPlate; }
            set
            {
                Set(() => BoilingPlate, ref _boilingPlate, value);
            }
        }

        public MainViewModel(IDateTimeModule dateTimeModule, IMixerModule mixerModule, ITemperatureModule temperatureModule, ITemperatureControlModule temperatureControlModule)
        {
            _dateTimeModule = dateTimeModule;
            _mixerModule = mixerModule;
            _temperatureModule = temperatureModule;
            _temperatureControlModule = temperatureControlModule;
            InitializeDateTimeTimer();
            InitializeTemperatureTimer();
            TemperatureConfigured = 50.0;
            TemperatureControl = false;
            InitializeTemperatureControlTimer();
            MixerStatus = false;
        }

        private void InitializeTemperatureTimer()
        {
            var timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            timer.Tick += (sender, o) => TemperatureCurrent = _temperatureModule.GetCurrenTemperature().Temperature;
            timer.Start();
        }

        private void InitializeDateTimeTimer()
        {
            var timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            timer.Tick += (sender, o) => DateTime = _dateTimeModule.GetCurrentDateTime().DateTime;
            timer.Start();
        }

        private void InitializeTemperatureControlTimer()
        {
            var timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            timer.Tick +=
                (sender, o) =>
                    BoilingPlate =
                        _temperatureControlModule.ControlTemperature(TemperatureControl, TemperatureConfigured,
                            TemperatureCurrent).Heating;
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

        //public RelayCommand TemperatureDownCommandHolding => new RelayCommand(TemperatureDownHolding);

        //private void TemperatureDownCommandHolding()
        //{

        //}

        //public void TemperatureDownCommandHolding(object sender, HoldingRoutedEventArgs e)
        //{

        //}
    }
}