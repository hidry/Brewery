using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    class BoilingPlate2Module : BoilingPlateModule, IBoilingPlate2Module
    {
        public BoilingPlate2Module(Settings settings) : base(settings.BoilingPlate2Gpio.GpioNumber)
        {
        }
    }
}