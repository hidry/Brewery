using Brewery.Server.Core;
using System.Diagnostics;

namespace Brewery.Server.Logic.RaspberryPi
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