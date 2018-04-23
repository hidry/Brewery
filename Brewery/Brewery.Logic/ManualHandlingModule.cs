using Brewery.UI.Core.Contracts;
using Brewery.Core.Contracts.ServiceAdapter;

namespace Brewery.UI.Logic
{
    public class ManualHandlingModule : IManualHandlingModule
    {
        private readonly IBoilingPlate1Service _boilingPlate1Module;
        private readonly IBoilingPlate2Service _boilingPlate2Module;
        private readonly IMixerService _mixerModule;
        private readonly IPiezoService _piezoModule;
        private readonly ITimer _timer;

        private int _boilingPlate1Temperature;
        private int _boilingPlate2Temperature;

        public ManualHandlingModule(ITimer timer, IBoilingPlate1Service boilingPlate1Module, IBoilingPlate2Service boilingPlate2Module, IMixerService mixerModule, IPiezoService piezoModule, IDevicesService devicesService)
        {
            _timer = timer;
            
            _boilingPlate1Module = boilingPlate1Module;
            _boilingPlate2Module = boilingPlate2Module;

            _mixerModule = mixerModule;
            _piezoModule = piezoModule;

            devicesService.Temperature1ChangedEvent += (sender, args) => BoilingPlate1Temperature = args.Temperature;
            devicesService.Temperature2ChangedEvent += (sender, args) => BoilingPlate2Temperature = args.Temperature;
        }

        private double BoilingPlate2Temperature { get; set; }

        private double BoilingPlate1Temperature { get; set; }

        private void ManageBoilingPlate1Temperature(int boilingPlate1Temperature)
        {
            _boilingPlate1Temperature = boilingPlate1Temperature;
            _boilingPlate1Module.ManageTemperature(boilingPlate1Temperature);
        }

        private void ManageBoilingPlate2Temperature(int boilingPlate2Temperature)
        {
            _boilingPlate2Temperature = boilingPlate2Temperature;
            _boilingPlate2Module.ManageTemperature(boilingPlate2Temperature);
        }

        public void StartBoilingPlate1TemperatureControl(int boilingPlate1Temperature)
        {
            _boilingPlate1Temperature = boilingPlate1Temperature;
            _timer.AddEvent(nameof(ManageBoilingPlate1Temperature), (o, e) => ManageBoilingPlate1Temperature(_boilingPlate1Temperature));
        }

        public void StartBoilingPlate2TemperatureControl(int boilingPlate2Temperature)
        {
            _boilingPlate2Temperature = boilingPlate2Temperature;
            _timer.AddEvent(nameof(ManageBoilingPlate2Temperature), (o, e) => ManageBoilingPlate2Temperature(_boilingPlate2Temperature));
        }

        public void StopBoilingPlate1TemperatureControl()
        {
            _timer.RemoveEvent(nameof(ManageBoilingPlate1Temperature), (o, e) => ManageBoilingPlate1Temperature(_boilingPlate1Temperature));
            _boilingPlate1Module.PowerOff();
        }

        public void StopBoilingPlate2()
        {
            _timer.RemoveEvent(nameof(ManageBoilingPlate2Temperature), (o, e) => ManageBoilingPlate2Temperature(_boilingPlate2Temperature));
            _boilingPlate2Module.PowerOff();
        }

        public void ChangeBoilingPlate1Temperature(int temperature)
        {
            _boilingPlate1Temperature += temperature;
        }

        public void ChangeBoilingPlate2Temperature(int temperature)
        {
            _boilingPlate2Temperature += temperature;
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