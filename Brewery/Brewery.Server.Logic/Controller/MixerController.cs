using Brewery.Server.Core;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;

namespace Brewery.Server.Logic.Controller
{
    [RestController(InstanceCreationType.Singleton)]
    class MixerController
    {
        private readonly IGpioModule _gpioModule;

        public MixerController()
        {
            _gpioModule = IocContainer.GetInstance<IGpioModule>();
        }

        [UriFormat("/mixer/power/{power}")]
        public IPutResponse Power(bool power)
        {
            _gpioModule.Power(12, power);
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }
    }
}