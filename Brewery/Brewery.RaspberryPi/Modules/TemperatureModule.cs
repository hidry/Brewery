using System.Collections.Generic;
using System.Linq;
using Brewery.Core.Contracts;
using Brewery.Core.Models;
using Rinsen.IoT.OneWire;

namespace Brewery.RaspberryPi.Modules
{
    public class TemperatureModule : ITemperatureModule
    {
        private IEnumerable<DS18B20> _devices;
        private OneWireDeviceHandler _handler;

        public TemperatureModule()
        {
            _handler = new OneWireDeviceHandler(false, false); //todo: dispose
            _devices = _handler.OneWireDevices.GetDevices<DS18B20>();
        }

        public TemperatureModel GetCurrenTemperature()
        {
            return new TemperatureModel() {Temperature = _devices.First().GetTemperature() };
        }
    }
}