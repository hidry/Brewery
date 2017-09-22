using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class PiezoModule : IPiezoModule
    {
        private readonly GpioModule _gpioModule;

        public PiezoModule(Settings settings)
        {
            _gpioModule = new GpioModule(settings.PiezoGpio.GpioNumber);
        }

        public void Power(bool on)
        {
            _gpioModule.Power(on);
        }
    }
}