using Brewery.Core.Models;

namespace Brewery.Core.Contracts
{
    public interface ITemperatureControlModule
    {
        TemperatureControlModel ControlTemperature(bool temperaureControlActive, double temperatureConfigured, double temperatureCurrent);
    }
}