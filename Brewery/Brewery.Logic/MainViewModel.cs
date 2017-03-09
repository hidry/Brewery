using System;
using Windows.UI.Xaml;
using Brewery.Core.Contracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Brewery.Logic
{
    public class MainViewModel : ViewModelBase
    {
        private const string On = "An"; //todo: Resource
        private const string Off = "Aus"; //todo: Resource
        private const string Visible = "Visible";
        private const string Collapsed = "Collapsed";
        private const double TemperatureSteps = 1.0;
        private readonly IDateTimeModule _dateTimeModule;
        private readonly IMixerModule _mixerModule;
        private readonly ITemperatureModule _temperatureModule;
        private readonly ITemperatureControlModule _temperatureControlModule;
        private string _dateTime;
        public string DateTime { get { return _dateTime; } set { Set(() => DateTime, ref _dateTime, value); } }

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

        private string _mixerStatus;
        public string MixerStatus { get { return _mixerStatus; } set { Set(() => MixerStatus, ref _mixerStatus, value); } }

        private bool _temperatureControl;
        public bool TemperatureControl
        {
            get { return _temperatureControl; }
            set
            {
                Set(() => TemperatureControl, ref _temperatureControl, value);
                Set(() => TemperatureControlStatus, ref _temperatureControlStatus, value ? On : Off);
            }
        }

        private string _temperatureControlStatus;
        public string TemperatureControlStatus { get { return _temperatureControlStatus; } set { Set(() => TemperatureControlStatus, ref _temperatureControlStatus, value); } }

        private string _boilingPlateStatus;
        public string BoilingPlateStatus
        {
            get { return _boilingPlateStatus; }
            set
            {
                Set(() => BoilingPlateStatus, ref _boilingPlateStatus, value);
            }
        }

        private string _temperatureControlChildElementsVisibility;
        public string TemperatureControlChildElementsVisibility
        {
            get { return _temperatureControlChildElementsVisibility; }
            set
            {
                Set(() => TemperatureControlChildElementsVisibility, ref _temperatureControlChildElementsVisibility,
                    value);
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
            TemperatureControlChildElementsVisibility = Collapsed;
            TemperatureControl = false;
            BoilingPlateStatus = Off;
            InitializeTemperatureControlTimer();
            MixerStatus = Off;
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
            timer.Tick += (sender, o) => DateTime = _dateTimeModule.GetCurrentDateTime().DateTime.ToString("H:mm:ss");
            timer.Start();
        }

        private void InitializeTemperatureControlTimer()
        {
            var timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            timer.Tick +=
                (sender, o) =>
                    BoilingPlateStatus =
                        _temperatureControlModule.ControlTemperature(TemperatureControl, TemperatureConfigured,
                            TemperatureCurrent).Heating
                            ? On
                            : Off;
            timer.Start();
        }

        private void ToggleMixer()
        {
            MixerStatus = _mixerModule.ToggleStatus().Status ? On : Off;
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
            if (TemperatureControlStatus == Off)
            {
                TemperatureControl = true;
                TemperatureControlChildElementsVisibility = Visible;
            }
            else
            {
                TemperatureControl = false;
                TemperatureControlChildElementsVisibility = Collapsed;
            }
        }
    }
}