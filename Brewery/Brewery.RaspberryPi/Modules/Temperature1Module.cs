using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class Temperature1Module : TemperatureModule, ITemperature1Module
    {
        public Temperature1Module(Settings settings) : base(settings.TemperatureSensor1OneWireAddress)
        {
        }
    }
}