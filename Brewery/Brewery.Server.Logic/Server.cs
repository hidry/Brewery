using Brewery.Server.Core;
using Brewery.Server.Core.Service;
using Brewery.Server.Logic.Api.Controller;
using Restup.Webserver.File;
using Restup.Webserver.Http;
using Restup.Webserver.Rest;
using System;
using System.Threading.Tasks;

namespace Brewery.Server.Logic
{
    class Server : IServer
    {
        private readonly IBoilingPlate1Worker _boilingPlate1Worker;
        private readonly IBoilingPlate2Worker _boilingPlate2Worker;

        public Server(IBoilingPlate1Worker boilingPlate1Worker, IBoilingPlate2Worker boilingPlate2Worker)
        {
            _boilingPlate1Worker = boilingPlate1Worker;
            _boilingPlate2Worker = boilingPlate2Worker;
        }

        public async Task StartServerAsync()
        {
            await Task.WhenAll(StartApiAsync(), StartBoilingPlate1WorkerAsync(), StartBoilingPlate2WorkerAsync());
        }

        private async Task StartApiAsync()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<PiezoController>();
            restRouteHandler.RegisterController<MixerController>();
            restRouteHandler.RegisterController<StatusController>();
            restRouteHandler.RegisterController<BoilingPlate1Controller>();
            restRouteHandler.RegisterController<BoilingPlate2Controller>();
            restRouteHandler.RegisterController<MashStepsController>();

            var configuration = new HttpServerConfiguration()
              .ListenOnPort(8800)
              .RegisterRoute("api", restRouteHandler)
              .RegisterRoute(new StaticFileRouteHandler(@"Web"))
              .EnableCors();

            var httpServer = new HttpServer(configuration);
            await httpServer.StartServerAsync();
        }

        private async Task StartWorkerAsync(Task workerTask, int intervall)
        {
            var backgroundService = new Task(async () =>
            {
                var dateTimeLastRun = default(DateTime);
                while (true)
                {
                    if (DateTime.Now - dateTimeLastRun >= new TimeSpan(0, 0, 0, intervall))
                    {
                        await workerTask;
                        dateTimeLastRun = DateTime.Now;
                    }
                }
            });
            backgroundService.Start();
            await backgroundService;
        }

        private async Task StartBoilingPlate1WorkerAsync()
        {
            //await StartWorkerAsync(_boilingPlate1Worker.Execute(), 3);
            var backgroundService = new Task(async () =>
            {
                var dateTimeLastRun = default(DateTime);
                while (true)
                {
                    if (DateTime.Now - dateTimeLastRun >= new TimeSpan(0, 0, 0, 3))
                    {
                        await _boilingPlate1Worker.Execute();
                        dateTimeLastRun = DateTime.Now;
                    }
                }
            });
            backgroundService.Start();
            await backgroundService;
        }

        private async Task StartBoilingPlate2WorkerAsync()
        {
            //await StartWorkerAsync(_boilingPlate2Worker.Execute(), 3);
            var backgroundService = new Task(async () =>
            {
                var dateTimeLastRun = default(DateTime);
                while (true)
                {
                    if (DateTime.Now - dateTimeLastRun >= new TimeSpan(0, 0, 0, 3))
                    {
                        await _boilingPlate2Worker.Execute();
                        dateTimeLastRun = DateTime.Now;
                    }
                }
            });
            backgroundService.Start();
            await backgroundService;
        }         
    }
}