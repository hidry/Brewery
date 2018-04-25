using Brewery.Server.Core;
using Brewery.Server.Core.Service;
using Brewery.Server.Logic.Api.Controller;
using Restup.Webserver.Http;
using Restup.Webserver.Rest;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Brewery.Server.Logic
{
    class Server : IServer
    {
        private readonly IMashService _mashService;

        public Server(IMashService mashService)
        {
            _mashService = mashService;
        }

        public async Task StartServerAsync()
        {
            await Task.WhenAll(StartApiAsync(), StartBackgroundServiceAsync());
        }

        private async Task StartApiAsync()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<PiezoController>();
            restRouteHandler.RegisterController<MixerController>();
            restRouteHandler.RegisterController<StatusController>();
            restRouteHandler.RegisterController<BoilingPlate1Controller>();
            restRouteHandler.RegisterController<BoilingPlate2Controller>();
            restRouteHandler.RegisterController<MashServiceController>();

            var configuration = new HttpServerConfiguration()
              .ListenOnPort(8800)
              .RegisterRoute("api", restRouteHandler)
              //.RegisterRoute(new StaticFileRouteHandler(@"Web"))
              .EnableCors();

            var httpServer = new HttpServer(configuration);
            await httpServer.StartServerAsync();
        }

        private async Task StartBackgroundServiceAsync()
        {
            var backgroundService = new Task(async () =>
            {
                var dateTimeLastRun = default(DateTime);
                while (true)
                {
                    if (DateTime.Now - dateTimeLastRun >= new TimeSpan(0, 0, 0, 1))
                    {
                        await _mashService.Execute();
                        dateTimeLastRun = DateTime.Now;
                    }
                }
            });
            backgroundService.Start();
            await backgroundService;
        }
    }
}