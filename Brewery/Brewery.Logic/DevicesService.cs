using System;
using Brewery.Core.Contracts;
using Brewery.Core.Contracts.ServiceAdapter;

namespace Brewery.Logic
{

    class DevicesService : IDevicesService
    {
        private readonly IBoilingPlate1Service _boilingPlate1Module;
        private readonly IBoilingPlate2Service _boilingPlate2Module;

        public DevicesService(IBoilingPlate1Service boilingPlate1Module, IBoilingPlate2Service boilingPlate2Module)
        {
            _boilingPlate1Module = boilingPlate1Module;
            _boilingPlate2Module = boilingPlate2Module;
        }

        public event EventHandler<TemperatureChangedEventArgs> Temperature1ChangedEvent;
        public event EventHandler<TemperatureChangedEventArgs> Temperature2ChangedEvent;
        public event EventHandler<HeatingStatusChangedEventArgs> HeatingStatus1ChangedEvent;
        public event EventHandler<HeatingStatusChangedEventArgs> HeatingStatus2ChangedEvent;

        public async void RefreshDeviceStatus()
        {
            var t = await _boilingPlate1Module.GetCurrenTemperature();
            OnTemperature1ChangedEvent(new TemperatureChangedEventArgs(t));

            var t2 = await _boilingPlate2Module.GetCurrenTemperature();
            OnTemperature2ChangedEvent(new TemperatureChangedEventArgs(t2));
            
            var h = await _boilingPlate1Module.GetPowerStatus();
            OnHeatingStatus1ChangedEvent(new HeatingStatusChangedEventArgs(h));
            
            var h2 = await _boilingPlate2Module.GetPowerStatus();
            OnHeatingStatus2ChangedEvent(new HeatingStatusChangedEventArgs(h2));           
        }

        protected virtual void OnTemperature1ChangedEvent(TemperatureChangedEventArgs e)
        {
            var handler = Temperature1ChangedEvent;
            handler?.Invoke(this, e);
        }

        protected virtual void OnTemperature2ChangedEvent(TemperatureChangedEventArgs e)
        {
            var handler = Temperature2ChangedEvent;
            handler?.Invoke(this, e);
        }

        protected virtual void OnHeatingStatus1ChangedEvent(HeatingStatusChangedEventArgs e)
        {
            var handler = HeatingStatus1ChangedEvent;
            handler?.Invoke(this, e);
        }

        protected virtual void OnHeatingStatus2ChangedEvent(HeatingStatusChangedEventArgs e)
        {
            var handler = HeatingStatus2ChangedEvent;
            handler?.Invoke(this, e);
        }
    }
}