namespace Brewery.Core.Contracts
{
    public interface IManualHandlingModule
    {
        void StartTemperatureControl1(int temperatureControl1Temperature);
        void StartTemperatureControl2(int temperatureControl2Temperature);
        void StopTemperatureControl1();
        void StopTemperatureControl2();
        void ChangeTemperature1(int temperature);
        void ChangeTemperature2(int temperature);
        void StartPizeoControl();
        void StopPizeoControl();
        void StopMixerControl();
        void StartMixerControl();
    }
}
