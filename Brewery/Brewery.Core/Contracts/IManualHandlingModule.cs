namespace Brewery.Core.Contracts
{
    public interface IManualHandlingModule
    {
        void StartBoilingPlate1TemperatureControl(int boilingPlate1Temperature);
        void StartBoilingPlate2TemperatureControl(int boilingPlate2Temperature);
        void StopBoilingPlate1TemperatureControl();
        void StopBoilingPlate2();
        void ChangeBoilingPlate1Temperature(int temperature);
        void ChangeBoilingPlate2Temperature(int temperature);
        void StartPizeoControl();
        void StopPizeoControl();
        void StopMixerControl();
        void StartMixerControl();
    }
}
