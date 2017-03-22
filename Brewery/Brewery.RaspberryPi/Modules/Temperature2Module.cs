using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class Temperature2Module : TemperatureModule, ITemperature2Module
    {
        public Temperature2Module(Settings settings) : base(settings.TemperatureSensor2OneWireAddress)
        {
        }
    }
}