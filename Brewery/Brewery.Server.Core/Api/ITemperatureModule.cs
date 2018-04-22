namespace Brewery.Server.Core.Api
{
    public interface ITemperatureModule
    {
        double GetCurrenTemperature(string oneWireAddressString);
    }
}