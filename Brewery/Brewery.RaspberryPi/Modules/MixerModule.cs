using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class MixerModule : IMixerModule
    {
        private readonly GpioModule _gpioModule;

        public MixerModule(Settings settings)
        {
            _gpioModule = new GpioModule(settings.MixerGpio.GpioNumber);
        }

        public void Power(bool on)
        {
            _gpioModule.Power(on);
        }
    }
}