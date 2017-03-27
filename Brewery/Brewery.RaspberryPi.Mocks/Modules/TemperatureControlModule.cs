using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class TemperatureControlModule : ITemperatureControlModule
    {
        private readonly TemperatureControlModel _temperatureControlModel = new TemperatureControlModel();

        public TemperatureControlModel ManageTemperature(double temperatureConfigured, double temperatureCurrent)
        {
            if (temperatureCurrent < temperatureConfigured)
            {
                _temperatureControlModel.Heating = true;
            }
            else
            {
                _temperatureControlModel.Heating = false;
            }
            return _temperatureControlModel;
        }

        public TemperatureControlModel GetStatus()
        {
            return _temperatureControlModel;
        }

        public void BoilingPlateOff()
        {
            _temperatureControlModel.Heating = false;
        }
    }
}