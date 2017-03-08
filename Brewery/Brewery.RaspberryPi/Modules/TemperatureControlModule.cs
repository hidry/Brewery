using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class TemperatureControlModule : ITemperatureControlModule
    {
        private readonly IBoilingPlateModule _boilingPlateModule;

        public TemperatureControlModule(IBoilingPlateModule boilingPlateModule)
        {
            _boilingPlateModule = boilingPlateModule;
        }

        public TemperatureControlModel ControlTemperature(bool temperaureControlActive, double temperatureConfigured, double temperatureCurrent)
        {
            var result = new TemperatureControlModel();
            if (!temperaureControlActive)
            {
                _boilingPlateModule.PowerOff();
                return result;
            }

            if (temperatureCurrent < temperatureConfigured)
            {
                _boilingPlateModule.PowerOn();
                result.Heating = true;
            }
            else
            {
                _boilingPlateModule.PowerOff();
                result.Heating = false;
            }
            return result;
        }
    }
}