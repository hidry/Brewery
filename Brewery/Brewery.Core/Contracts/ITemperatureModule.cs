using Brewery.Core.Models;

namespace Brewery.Core.Contracts
{
    public interface ITemperatureModule
    {
        TemperatureModel GetCurrenTemperature();
    }
}