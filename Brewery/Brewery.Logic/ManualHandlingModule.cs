using Brewery.Core.Contracts;

namespace Brewery.Logic
{
    public class ManualHandlingModule : IManualHandlingModule
    {
        private readonly ITemperature1Module _temperature1Module;
        private readonly ITemperatureControl1Module _temperatureControl1Module;
        private readonly ITemperature2Module _temperature2Module;
        private readonly ITemperatureControl2Module _temperatureControl2Module;
        private readonly ITimer _timer;

        private int _temperatureControl1Temperature;
        private int _temperatureControl2Temperature;

        public ManualHandlingModule(ITimer timer, ITemperature1Module temperature1Module, ITemperatureControl1Module temperatureControl1Module, ITemperature2Module temperature2Module, ITemperatureControl2Module temperatureControl2Module)
        {
            _timer = timer;

            _temperature1Module = temperature1Module;
            _temperatureControl1Module = temperatureControl1Module;

            _temperature2Module = temperature2Module;
            _temperatureControl2Module = temperatureControl2Module;
        }

        private void ManageTemperature1(int temperatureControl1Temperature)
        {
            _temperatureControl1Temperature = temperatureControl1Temperature;
            _temperatureControl1Module.ManageTemperature(temperatureControl1Temperature, _temperature1Module.GetCurrenTemperature().Temperature);
        }

        private void ManageTemperature2(int temperatureControl2Temperature)
        {
            _temperatureControl2Temperature = temperatureControl2Temperature;
            _temperatureControl2Module.ManageTemperature(temperatureControl2Temperature, _temperature2Module.GetCurrenTemperature().Temperature);
        }

        public void StartTemperatureControl1(int temperatureControl1Temperature)
        {
            _temperatureControl1Temperature = temperatureControl1Temperature;
            _timer.AddEvent(nameof(ManageTemperature1), (o, e) => ManageTemperature1(_temperatureControl1Temperature));
        }

        public void StartTemperatureControl2(int temperatureControl2Temperature)
        {
            _temperatureControl2Temperature = temperatureControl2Temperature;
            _timer.AddEvent(nameof(ManageTemperature2), (o, e) => ManageTemperature2(_temperatureControl2Temperature));
        }

        public void StopTemperatureControl1()
        {
            _timer.RemoveEvent(nameof(ManageTemperature1), (o, e) => ManageTemperature1(_temperatureControl1Temperature));
            _temperatureControl1Module.BoilingPlateOff();
        }

        public void StopTemperatureControl2()
        {
            _timer.RemoveEvent(nameof(ManageTemperature2), (o, e) => ManageTemperature2(_temperatureControl2Temperature));
            _temperatureControl2Module.BoilingPlateOff();
        }

        public void ChangeTemperature1(int temperature)
        {
            _temperatureControl1Temperature += temperature;
        }

        public void ChangeTemperature2(int temperature)
        {
            _temperatureControl2Temperature += temperature;
        }
    }
}