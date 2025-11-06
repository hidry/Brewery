using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;

namespace Brewery.Server.Logic.Api.Controller
{
    [RestController(InstanceCreationType.Singleton)]
    class StatusController
    {
        [UriFormat("/serverStatus")]
        public IGetResponse GetStatus()
        {
            return new GetResponse(GetResponse.ResponseStatus.OK, new { Message = "Server is up and running..." });
        }
    }
}