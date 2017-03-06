using System.Diagnostics;
using Windows.Devices.Gpio;
using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.Modules
{
    class MixerModule : IMixerModule
    {
        private GpioController _gpioController;
        private GpioPin _gpio12; //pin 32
        private bool _running;

        public MixerModule()
        {
            _gpioController = GpioController.GetDefault();
            if (_gpioController == null)
            {
                Debug.WriteLine("No gpio controller available.");
            }
            else
            {
                InitGpio12();
            }
        }

        private void InitGpio12()
        {
            _gpio12 = _gpioController.OpenPin(12);
            _gpio12.Write(GpioPinValue.Low);
            _gpio12.SetDriveMode(GpioPinDriveMode.Output);
        }

        public MixerModel ToggleStatus()
        {
            if (_running)
            {
                _gpio12?.Write(GpioPinValue.Low);
            }
            else
            {
                _gpio12?.Write(GpioPinValue.High);
            }
            _running = !_running;
            return new MixerModel() { Status = _running };
        }
    }
}