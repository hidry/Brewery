using Brewery.Core.Models;

namespace Brewery.Core.Contracts
{
    interface ITemperatureModule
    {
        TemperatureModel GetCurrenTemperature();
    }
}