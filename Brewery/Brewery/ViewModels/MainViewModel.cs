using System;
using System.Linq;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Brewery.Core.Contracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Brewery.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private readonly IDateTimeModule _dateTimeModule;
        private readonly IMixerModule _mixerModule;
        private readonly ITemperatureModule _temperatureModule;
        private DateTime _dateTime;
        public DateTime DateTime { get { return _dateTime; } set { Set(() => DateTime, ref _dateTime, value); } }

        private double _temperatureCurrent;
        public double TemperatureCurrent { get {return _temperatureCurrent;} set { Set(() => TemperatureCurrent, ref _temperatureCurrent, value); } }

        private double _temperatureConfigured;
        public double TemperatureConfigured { get { return _temperatureConfigured; } set { Set(() => TemperatureConfigured, ref _temperatureConfigured, value); } }

        private string _mixerStatus;
        public string MixerStatus { get{return _mixerStatus;} set { Set(() => MixerStatus, ref _mixerStatus, value); } }


        public MainViewModel(IDateTimeModule dateTimeModule, IMixerModule mixerModule, ITemperatureModule temperatureModule)
        {
            _dateTimeModule = dateTimeModule;
            _mixerModule = mixerModule;
            _temperatureModule = temperatureModule;
            InitializeDateTimeTimer();
            InitializeTemperatureTimer();
            TemperatureConfigured = 50.0;
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

        private void ToggleMixer()
        {
            MixerStatus = _mixerModule.ToggleStatus().Status ? "An" : "Aus"; //todo: Resource
        }

        public RelayCommand ToggleMixerCommand => new RelayCommand(ToggleMixer);

        public RelayCommand TemperatureDownCommand => new RelayCommand(TemperatureDown);

        private void TemperatureDown()
        {
            TemperatureConfigured -= 1.0;
        }

        public RelayCommand TemperatureUpCommand => new RelayCommand(TemperatureUp);

        private void TemperatureUp()
        {
            TemperatureConfigured += 1.0;
        }
    }
}