namespace Brewery.Server.Core
{
    public interface ITemperatureModule
    {
        double GetCurrenTemperature(string oneWireAddressString);
    }
}