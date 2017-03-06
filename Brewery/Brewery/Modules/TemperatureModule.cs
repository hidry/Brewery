using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Brewery.Core.Contracts;
using Brewery.Core.Models;
using Rinsen.IoT.OneWire;

namespace Brewery.Modules
{
    class TemperatureModule : ITemperatureModule
    {
        private IEnumerable<DS18B20> _devices;

        public TemperatureModule()
        {
            using (var handler = new OneWireDeviceHandler(false, false))
            {
                _devices = handler.OneWireDevices.GetDevices<DS18B20>();
            }
                
            if (_devices == null || !_devices.Any())
            {
                Debug.WriteLine("No temperature sensor (DS18B20) available.");
            }
        }

        public TemperatureModel GetCurrenTemperature()
        {
            return new TemperatureModel() {Temperature = _devices.First().GetTemperature() };
        }
    }
}