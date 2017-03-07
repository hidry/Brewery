using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class MixerModule : IMixerModule
    {
        private bool _running;
        
        public MixerModel ToggleStatus()
        {
            _running = !_running;
            return new MixerModel() { Status = _running };
        }
    }
}