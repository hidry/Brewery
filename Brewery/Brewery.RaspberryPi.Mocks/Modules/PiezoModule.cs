using Brewery.Core.Contracts;

namespace Brewery.RaspberryPi.Modules
{
    public class PiezoModule : IPiezoModule
    {
        public void Power(bool on)
        {
            ModuleDelay.Sleep();
        }
    }
}