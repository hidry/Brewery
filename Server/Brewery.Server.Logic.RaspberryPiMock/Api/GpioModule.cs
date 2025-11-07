using Brewery.Server.Core.Api;
using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace Brewery.Server.Logic.RaspberryPiMock.Api
{
    public class GpioModule : IGpioModule
    {
        private readonly Dictionary<int, bool> _gpioPins = new Dictionary<int, bool>();
        
        public bool GetValue(int gpioName)
        {
            return GetElement(gpioName);
        }

        public void Power(int gpioName, bool on)
        {
            SetElement(gpioName, on);
            Debug.WriteLine($"{DateTime.Now} {gpioName} {on}");
        }

        private bool SetElement(int gpioName, bool power)
        {
            if (!_gpioPins.ContainsKey(gpioName))
                _gpioPins.Add(gpioName, power);
            return _gpioPins[gpioName];
        }

        private bool GetElement(int gpioName)
        {
            if (!_gpioPins.ContainsKey(gpioName))
                _gpioPins.Add(gpioName, false);
            return _gpioPins[gpioName];
        }
    }
}