using Windows.Devices.Gpio;

namespace Brewery.RaspberryPi.Modules
{
    class GpioModule
    {
        private readonly GpioPin _gpioPin;

        public GpioModule(int gpioName)
        {
            var gpioController = GpioController.GetDefault();
            _gpioPin = gpioController.OpenPin(gpioName);
            _gpioPin.Write(GpioPinValue.Low);
            _gpioPin.SetDriveMode(GpioPinDriveMode.Output);
        }

        public void Power(bool on)
        {
            _gpioPin.Write(on ? GpioPinValue.High : GpioPinValue.Low);
        }
    }
}