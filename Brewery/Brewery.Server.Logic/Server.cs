using Brewery.Server.Core;
using Brewery.Server.Logic.Controller;
using Restup.Webserver.Http;
using Restup.Webserver.Rest;
using System.Threading.Tasks;

namespace Brewery.Server.Logic
{
    class Server : IServer
    {
        public async Task StartServerAsync()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<PiezoController>();
            restRouteHandler.RegisterController<MixerController>();
            restRouteHandler.RegisterController<StatusController>();
            restRouteHandler.RegisterController<BoilingPlate1Controller>();
            restRouteHandler.RegisterController<BoilingPlate2Controller>();

            var configuration = new HttpServerConfiguration()
              .ListenOnPort(8800)
              .RegisterRoute("api", restRouteHandler)
              //.RegisterRoute(new StaticFileRouteHandler(@"Web"))
              .EnableCors();

            var httpServer = new HttpServer(configuration);
            await httpServer.StartServerAsync();
        }
    }
}