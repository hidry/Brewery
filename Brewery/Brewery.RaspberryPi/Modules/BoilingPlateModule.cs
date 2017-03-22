using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public abstract class BoilingPlateModule : IBoilingPlateModule
    {
        private readonly GpioModule _gpioModule;

        public BoilingPlateModule(int gpioNumber)
        {
            _gpioModule = new GpioModule(gpioNumber);
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