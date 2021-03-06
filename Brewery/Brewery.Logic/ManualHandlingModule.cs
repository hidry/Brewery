﻿using Brewery.Core.Contracts;

namespace Brewery.Logic
{
    public class ManualHandlingModule : IManualHandlingModule
    {
        private readonly ITemperatureControl1Module _temperatureControl1Module;
        private readonly ITemperatureControl2Module _temperatureControl2Module;
        private readonly IMixerModule _mixerModule;
        private readonly IPiezoModule _piezoModule;
        private readonly ITimer _timer;

        private int _temperatureControl1Temperature;
        private int _temperatureControl2Temperature;

        public ManualHandlingModule(ITimer timer, ITemperatureControl1Module temperatureControl1Module, ITemperatureControl2Module temperatureControl2Module, IMixerModule mixerModule, IPiezoModule piezoModule, IDevicesService devicesService)
        {
            _timer = timer;
            
            _temperatureControl1Module = temperatureControl1Module;
            _temperatureControl2Module = temperatureControl2Module;

            _mixerModule = mixerModule;
            _piezoModule = piezoModule;

            devicesService.Temperature1ChangedEvent += (sender, args) => Temperature1 = args.Temperature;
            devicesService.Temperature2ChangedEvent += (sender, args) => Temperature2 = args.Temperature;
        }

        private double Temperature2 { get; set; }

        private double Temperature1 { get; set; }

        private void ManageTemperature1(int temperatureControl1Temperature)
        {
            _temperatureControl1Temperature = temperatureControl1Temperature;
            _temperatureControl1Module.ManageTemperature(temperatureControl1Temperature, Temperature1);
        }

        private void ManageTemperature2(int temperatureControl2Temperature)
        {
            _temperatureControl2Temperature = temperatureControl2Temperature;
            _temperatureControl2Module.ManageTemperature(temperatureControl2Temperature, Temperature2);
        }

        public void StartTemperatureControl1(int temperatureControl1Temperature)
        {
            _temperatureControl1Temperature = temperatureControl1Temperature;
            _timer.AddEvent(nameof(ManageTemperature1), (o, e) => ManageTemperature1(_temperatureControl1Temperature));
        }

        public void StartTemperatureControl2(int temperatureControl2Temperature)
        {
            _temperatureControl2Temperature = temperatureControl2Temperature;
            _timer.AddEvent(nameof(ManageTemperature2), (o, e) => ManageTemperature2(_temperatureControl2Temperature));
        }

        public void StopTemperatureControl1()
        {
            _timer.RemoveEvent(nameof(ManageTemperature1), (o, e) => ManageTemperature1(_temperatureControl1Temperature));
            _temperatureControl1Module.BoilingPlateOff();
        }

        public void StopTemperatureControl2()
        {
            _timer.RemoveEvent(nameof(ManageTemperature2), (o, e) => ManageTemperature2(_temperatureControl2Temperature));
            _temperatureControl2Module.BoilingPlateOff();
        }

        public void ChangeTemperature1(int temperature)
        {
            _temperatureControl1Temperature += temperature;
        }

        public void ChangeTemperature2(int temperature)
        {
            _temperatureControl2Temperature += temperature;
        }

        public void StartPizeoControl()
        {
            _piezoModule.Power(true);
        }

        public void StopPizeoControl()
        {
            _piezoModule.Power(false);
        }

        public void StopMixerControl()
        {
            _mixerModule.Power(false);
        }

        public void StartMixerControl()
        {
            _mixerModule.Power(true);
        }
    }
}