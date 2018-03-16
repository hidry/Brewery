using System;
using System.Threading.Tasks;
using Brewery.Core.Contracts;

namespace Brewery.Logic
{

    class DevicesService : IDevicesService
    {
        private readonly ITemperature1Module _temperature1Module;
        private readonly ITemperature2Module _temperature2Module;
        private readonly ITemperatureControl1Module _temperatureControl1Module;
        private readonly ITemperatureControl2Module _temperatureControl2Module;

        public DevicesService(ITemperature1Module temperature1Module, ITemperature2Module temperature2Module, ITemperatureControl1Module temperatureControl1Module, ITemperatureControl2Module temperatureControl2Module)
        {
            _temperature1Module = temperature1Module;
            _temperature2Module = temperature2Module;
            _temperatureControl1Module = temperatureControl1Module;
            _temperatureControl2Module = temperatureControl2Module;
        }

        public event EventHandler<TemperatureChangedEventArgs> Temperature1ChangedEvent;
        public event EventHandler<TemperatureChangedEventArgs> Temperature2ChangedEvent;
        public event EventHandler<HeatingStatusChangedEventArgs> HeatingStatus1ChangedEvent;
        public event EventHandler<HeatingStatusChangedEventArgs> HeatingStatus2ChangedEvent;

        public async void RefreshTemperatures()
        {
            var t1 = Task.Run(() =>
            {
                return _temperature1Module.GetCurrenTemperature().Temperature;
            });
            var t2 = Task.Run(() =>
            {
                return _temperature2Module.GetCurrenTemperature().Temperature;
            });
            var h1 = Task.Run(() =>
            {
                return _temperatureControl1Module.GetStatus().Heating;
            });
            var h2 = Task.Run(() =>
            {
                return _temperatureControl2Module.GetStatus().Heating;
            });
            
            await Task.WhenAll(t1, t2, h1, h2);
            
            OnTemperature1ChangedEvent(new TemperatureChangedEventArgs(t1.Result));
            OnTemperature2ChangedEvent(new TemperatureChangedEventArgs(t2.Result));
            OnHeatingStatus1ChangedEvent(new HeatingStatusChangedEventArgs(h1.Result));
            OnHeatingStatus2ChangedEvent(new HeatingStatusChangedEventArgs(h2.Result));
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