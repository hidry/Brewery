using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class MixerModule : IMixerModule
    {
        private readonly GpioModule _gpioModule;
        private bool _running;

        public MixerModule()
        {
            _gpioModule = new GpioModule(12); //pin 32
        }

        public MixerModel ToggleStatus()
        {
            if (_running)
            {
                _gpioModule.Power(false);
            }
            else
            {
                _gpioModule.Power(true);
            }
            _running = !_running;
            return new MixerModel() { Status = _running };
        }
    }
}