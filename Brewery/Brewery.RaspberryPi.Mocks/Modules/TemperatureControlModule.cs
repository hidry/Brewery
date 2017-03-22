using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class TemperatureControlModule : ITemperatureControlModule
    {
        public TemperatureControlModel ManageTemperature(double temperatureConfigured, double temperatureCurrent)
        {
            return new TemperatureControlModel();
        }
    }
}