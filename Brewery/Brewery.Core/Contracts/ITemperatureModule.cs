using System;
using Brewery.Core.Models;

namespace Brewery.Core.Contracts
{
    public interface ITemperatureModule : IDisposable
    {
        TemperatureModel GetCurrenTemperature();
    }
}