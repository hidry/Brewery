using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class TemperatureModule : ITemperatureModule
    {
        public TemperatureModel GetCurrenTemperature()
        {
            return new TemperatureModel() {Temperature = 25.0154 };
        }
    }
}