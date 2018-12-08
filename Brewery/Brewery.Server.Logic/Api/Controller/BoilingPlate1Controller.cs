using Brewery.Core;
using Brewery.Server.Core.Api;
using Brewery.Server.Core.Service;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using System;

namespace Brewery.Server.Logic.Api.Controller
{
    [RestController(InstanceCreationType.Singleton)]
    class BoilingPlate1Controller
    {
        private readonly IBoilingPlate1Worker _boilingPlate1Worker;
        private readonly IGpioModule _gpioModule;
        private readonly ITemperatureModule _temperatureModule;

        public BoilingPlate1Controller()
        {
            _gpioModule = IocContainer.GetInstance<IGpioModule>();
            _temperatureModule = IocContainer.GetInstance<ITemperatureModule>();
            _boilingPlate1Worker = IocContainer.GetInstance<IBoilingPlate1Worker>();
        }

        [UriFormat("/boilingPlate1/powerStatus")]
        public IGetResponse GetPowerStatus()
        {
            return new GetResponse(GetResponse.ResponseStatus.OK, _boilingPlate1Worker.GetPowerStatus());
        }
        [UriFormat("/boilingPlate1/getCurrentTemperature")]
        public IGetResponse GetCurrenTemperature()
        {
            return new GetResponse(GetResponse.ResponseStatus.OK, GetTemperature());
        }
        private double GetTemperature()
        {
            return _temperatureModule.GetCurrenTemperature(Settings.TemperatureSensor1OneWireAddress);
        }
        [UriFormat("/boilingPlate1/messageAcknowledged")]
        public IPutResponse MessageAcknowledged()
        {
            _boilingPlate1Worker.MessageAcknowledged();
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }

        [UriFormat("/boilingPlate1/startMashProcess")]
        public IPutResponse StartMashProcess()
        {
            _boilingPlate1Worker.StartMashProcess();
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }

        [UriFormat("/boilingPlate1/stopMashProcess")]
        public IPutResponse StopMashProcess()
        {
            _boilingPlate1Worker.StopMashProcess();
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }
    }
}