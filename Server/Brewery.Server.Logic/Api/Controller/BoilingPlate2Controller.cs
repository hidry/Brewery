using Brewery.Core;
using Brewery.Server.Core.Api;
using Brewery.Server.Core.Models;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using System;

namespace Brewery.Server.Logic.Api.Controller
{
    [RestController(InstanceCreationType.Singleton)]
    class BoilingPlate2Controller
    {
        private readonly IGpioModule _gpioModule;
        private readonly ITemperatureModule _temperatureModule;
        private readonly BoilingPlate2Model _boilingPlate2Model;

        public BoilingPlate2Controller()
        {
            _gpioModule = IocContainer.GetInstance<IGpioModule>();
            _temperatureModule = IocContainer.GetInstance<ITemperatureModule>();
            _boilingPlate2Model = IocContainer.GetInstance<BoilingPlate2Model>();
        }

        [UriFormat("/boilingPlate2/powerStatus")]
        public IGetResponse GetPowerStatus()
        {
            return new GetResponse(GetResponse.ResponseStatus.OK, _boilingPlate2Model.PowerStatus);
        }
        [UriFormat("/boilingPlate2/power/{on}")]
        public IPutResponse Power(bool on)
        {
            _boilingPlate2Model.PowerStatus = on;
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }
        [UriFormat("/boilingPlate2/getCurrentTemperature")]
        public IGetResponse GetCurrenTemperature()
        {
            return new GetResponse(GetResponse.ResponseStatus.OK, GetCurrentTemperature());
        }
        private double GetCurrentTemperature()
        {
            return _temperatureModule.GetCurrenTemperature(Settings.TemperatureSensor2OneWireAddress);
        }
        [UriFormat("/boilingPlate2/setTemperature/{temperature}")]
        public IPutResponse SetTemperature(double temperature)
        {
            _boilingPlate2Model.Temperature = temperature;
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }
        [UriFormat("/boilingPlate2/getTemperature")]
        public IGetResponse GetTemperature()
        {
            return new GetResponse(GetResponse.ResponseStatus.OK, _boilingPlate2Model.Temperature);
        }
    }
}