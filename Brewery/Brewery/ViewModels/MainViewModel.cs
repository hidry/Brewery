using System;
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

        private double _temperature;
        public double Temperature { get {return _temperature;} set { Set(() => Temperature, ref _temperature, value); } }

        private string _mixerStatus;
        public string MixerStatus { get{return _mixerStatus;} set { Set(() => MixerStatus, ref _mixerStatus, value); } }


        public MainViewModel(IDateTimeModule dateTimeModule, IMixerModule mixerModule, ITemperatureModule temperatureModule)
        {
            _dateTimeModule = dateTimeModule;
            _mixerModule = mixerModule;
            _temperatureModule = temperatureModule;
            InitializeDateTimeTimer();
            InitializeTemperatureTimer();
        }

        private void InitializeTemperatureTimer()
        {
            var timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            timer.Tick += (sender, o) => Temperature = _temperatureModule.GetCurrenTemperature().Temperature;
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
    }
}