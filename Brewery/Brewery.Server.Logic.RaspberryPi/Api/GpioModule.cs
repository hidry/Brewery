using System.Collections.Generic;
using Windows.Devices.Gpio;
using Brewery.Server.Core.Api;

namespace Brewery.Server.Logic.RaspberryPi.Api
{
    public class GpioModule : IGpioModule
    {
        private readonly object _locker = new object();
        private readonly GpioController _gpioController = GpioController.GetDefault();
        private readonly Dictionary<int, GpioPin> _gpioPins = new Dictionary<int, GpioPin>();
        
        public void Power(int gpioName, bool on)
        {
            lock (_locker)
            {
                GetPin(gpioName).Write(on ? GpioPinValue.High : GpioPinValue.Low);
            }
        }
        
        public bool GetValue(int gpioName)
        {
            lock (_locker)
            {
                return GetPin(gpioName).Read() == GpioPinValue.High ? true : false;
            }
        }

        private GpioPin GetPin(int gpioName)
        {
            if (!_gpioPins.ContainsKey(gpioName))
            {
                var gpioPin = _gpioController.OpenPin(gpioName);
                gpioPin.SetDriveMode(GpioPinDriveMode.Output);
                _gpioPins.Add(gpioName, gpioPin);
            }
            return _gpioPins[gpioName];
        }
    }
}