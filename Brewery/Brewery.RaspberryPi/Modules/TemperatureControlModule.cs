using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class TemperatureControlModule : ITemperatureControlModule
    {
        private readonly IBoilingPlateModule _boilingPlateModule;
        private readonly TemperatureControlModel _temperatureControlModel = new TemperatureControlModel();

        public TemperatureControlModule(IBoilingPlateModule boilingPlateModule)
        {
            _boilingPlateModule = boilingPlateModule;
        }

        public TemperatureControlModel ControlTemperature(bool temperaureControlActive, double temperatureConfigured, double temperatureCurrent)
        {
            if (!temperaureControlActive)
            {
                if (!_temperatureControlModel.Heating)
                    return _temperatureControlModel;

                _boilingPlateModule.PowerOff();
                _temperatureControlModel.Heating = false;
                return _temperatureControlModel;
            }

            if (temperatureCurrent < temperatureConfigured)
            {
                _boilingPlateModule.PowerOn();
                _temperatureControlModel.Heating = true;
            }
            else
            {
                _boilingPlateModule.PowerOff();
                _temperatureControlModel.Heating = false;
            }
            return _temperatureControlModel;
        }
    }
}