﻿using Brewery.Server.Core;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;

namespace Brewery.Server.Logic.Controller
{
    [RestController(InstanceCreationType.Singleton)]
    class PiezoController
    {
        private readonly IGpioModule _gpioModule;

        public PiezoController()
        {
            _gpioModule = IocContainer.GetInstance<IGpioModule>();
        }

        [UriFormat("/piezo/power/{power}")]
        public IPutResponse Power(bool power)
        {
            _gpioModule.Power(21, power);
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }
    }
}