using Brewery.Server.Core.Api;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using System;

namespace Brewery.Server.Logic.Api.Controller
{
    [RestController(InstanceCreationType.Singleton)]
    class BoilingPlate1Controller
    {
        private readonly IGpioModule _gpioModule;
        private readonly ITemperatureModule _temperatureModule;

        public BoilingPlate1Controller()
        {
            _gpioModule = IocContainer.GetInstance<IGpioModule>();
            _temperatureModule = IocContainer.GetInstance<ITemperatureModule>();
        }

        [UriFormat("/boilingPlate1/powerStatus")]
        public IGetResponse GetPowerStatus()
        {
            var status = _gpioModule.GetValue(Settings.BoilingPlate1Gpio.GpioNumber);
            return new GetResponse(GetResponse.ResponseStatus.OK, new { Value = status });
        }
        [UriFormat("/boilingPlate1/power/{on}")]
        public IPutResponse Power(bool on)
        {
            _gpioModule.Power(Settings.BoilingPlate1Gpio.GpioNumber, on);
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }
        [UriFormat("/boilingPlate1/getCurrentTemperature")]
        public IGetResponse GetCurrenTemperature()
        {
            return new GetResponse(GetResponse.ResponseStatus.OK, new { Value = GetTemperature() });
        }
        private double GetTemperature()
        {
            return _temperatureModule.GetCurrenTemperature(Settings.TemperatureSensor1OneWireAddress);
        }
        [UriFormat("/boilingPlate1/manageTemperature/{temperature}")]
        public IPutResponse ManageTemperature(double temperature)
        {
            if (GetTemperature() < temperature)
            {
                Power(true);
            }
            else
            {
                Power(false);
            }
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }        
    }
}