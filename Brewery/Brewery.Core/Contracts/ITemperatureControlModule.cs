using Brewery.Core.Models;

namespace Brewery.Core.Contracts
{
    public interface ITemperatureControlModule
    {
        TemperatureControlModel ManageTemperature(double temperatureConfigured, double temperatureCurrent);
        TemperatureControlModel GetStatus();
        void BoilingPlateOff();
    }
}