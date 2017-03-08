using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class BoilingPlateModule : IBoilingPlateModule
    {
        private readonly GpioModule _gpioModule;

        public BoilingPlateModule()
        {
            _gpioModule = new GpioModule(16); // Pin 36 
        }

        public BoilingPlateModel PowerOn()
        {
            _gpioModule.Power(true);
            return new BoilingPlateModel() { Status = true };
        }

        public BoilingPlateModel PowerOff()
        {
            _gpioModule.Power(false);
            return new BoilingPlateModel() { Status = false };
        }
    }
}