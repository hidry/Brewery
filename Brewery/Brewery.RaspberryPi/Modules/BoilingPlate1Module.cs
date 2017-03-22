using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class BoilingPlate1Module : BoilingPlateModule, IBoilingPlate1Module
    {
        public BoilingPlate1Module(Settings settings) : base(settings.BoilingPlate1Gpio.GpioNumber)
        {
        }
    }
}