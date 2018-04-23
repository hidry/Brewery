using Brewery.Core;
using Brewery.Server.Core.Service;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;

namespace Brewery.Server.Logic.Api.Controller
{
    [RestController(InstanceCreationType.Singleton)]
    class MashServiceController
    {
        private readonly IMashService _mashService;

        public MashServiceController()
        {
            _mashService = IocContainer.GetInstance<IMashService>();
        }

        [UriFormat("/mashService/Status")]
        public IGetResponse GetStatus()
        {
            var status = _mashService.GetStatus();
            return new GetResponse(GetResponse.ResponseStatus.OK, new { Value = status });
        }

        [UriFormat("/mashService/messageAcknowledged")]
        public IPutResponse MessageAcknowledged()
        {
            _mashService.MessageAcknowledged();
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }

        [UriFormat("/mashService/pauseMashProcess")]
        public IPutResponse PauseMashProcess()
        {
            _mashService.PauseMashProcess();
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }

        [UriFormat("/mashService/startMashProcess")]
        public IPutResponse StartMashProcess()
        {
            _mashService.StartMashProcess();
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }

        [UriFormat("/mashService/stopMashProcess")]
        public IPutResponse StopMashProcess()
        {
            _mashService.StopMashProcess();
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }
    }
}