using System.Collections.Generic;
using System.Device.Gpio;
using Brewery.Server.Core.Api;

namespace Brewery.Server.Logic.RaspberryPi.Api
{
    public class GpioModule : IGpioModule
    {
        private readonly object _locker = new object();
        private readonly GpioController _gpioController = new GpioController();
        private readonly Dictionary<int, int> _gpioPins = new Dictionary<int, int>();

        public void Power(int gpioName, bool on)
        {
            lock (_locker)
            {
                OpenPinIfNeeded(gpioName);
                _gpioController.Write(gpioName, on ? PinValue.High : PinValue.Low);
            }
        }

        public bool GetValue(int gpioName)
        {
            lock (_locker)
            {
                OpenPinIfNeeded(gpioName);
                return _gpioController.Read(gpioName) == PinValue.High;
            }
        }

        private void OpenPinIfNeeded(int gpioName)
        {
            if (!_gpioPins.ContainsKey(gpioName))
            {
                _gpioController.OpenPin(gpioName, PinMode.Output);
                _gpioPins.Add(gpioName, gpioName);
            }
        }
    }
}