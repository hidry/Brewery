using Brewery.Core.Contracts;

namespace Brewery.RaspberryPi.Modules
{
    public class MixerModule : IMixerModule
    {
        public void Power(bool on)
        {
            ModuleDelay.Sleep($"MixerModule Power {on}");
        }
    }
}