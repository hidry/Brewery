namespace Brewery.Server.Core.Api
{
    public interface IGpioModule
    {
        void Power(int gpioName, bool on);
        bool GetValue(int gpioName);
    }
}