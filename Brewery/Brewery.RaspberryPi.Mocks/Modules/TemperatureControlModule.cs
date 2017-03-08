using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class TemperatureControlModule : ITemperatureControlModule
    {
        public TemperatureControlModel ControlTemperature(bool temperaureControlActive, double temperatureConfigured, double temperatureCurrent)
        {
            if (!temperaureControlActive)
                return new TemperatureControlModel();
            return temperatureCurrent < temperatureConfigured ? new TemperatureControlModel() { Heating = true } : new TemperatureControlModel();
        }
    }
}