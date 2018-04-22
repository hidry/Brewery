using Brewery.Server.Core.Api;
using System.Diagnostics;

namespace Brewery.Server.Logic.RaspberryPi.Api
{
    public class TemperatureModule : ITemperatureModule
    {
        public double GetCurrenTemperature(string oneWireAddressString)
        {
            Debug.WriteLine($"{oneWireAddressString}");
            return 25.2154;
        }
    }
}