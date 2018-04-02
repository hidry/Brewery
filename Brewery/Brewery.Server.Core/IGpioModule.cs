namespace Brewery.Server.Core
{
    public interface IGpioModule
    {
        void Power(int gpioName, bool on);
        bool GetValue(int gpioName);
    }
}