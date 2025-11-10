using Brewery.Server.Core;
using Brewery.Server.Core.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Brewery.Server.Logic
{
    public class Server : IServer
    {
        private readonly IBoilingPlate1Worker _boilingPlate1Worker;
        private readonly IBoilingPlate2Worker _boilingPlate2Worker;

        public Server(IBoilingPlate1Worker boilingPlate1Worker, IBoilingPlate2Worker boilingPlate2Worker)
        {
            _boilingPlate1Worker = boilingPlate1Worker;
            _boilingPlate2Worker = boilingPlate2Worker;
        }

        public async Task StartServerAsync(int port = 8800)
        {
            var builder = WebApplication.CreateBuilder();

            // Configure services
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(Server).Assembly);
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure middleware
            app.UseCors();

            // Enable default files (index.html) for root path
            app.UseDefaultFiles(new DefaultFilesOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Web")),
                RequestPath = ""
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Web")),
                RequestPath = ""
            });

            app.MapControllers();

            // Start workers
            _ = Task.Run(() => StartBoilingPlate1WorkerAsync());
            _ = Task.Run(() => StartBoilingPlate2WorkerAsync());

            // Start web server
            Console.WriteLine($"Starting web server on http://0.0.0.0:{port}");

            // Use StartAsync instead of RunAsync to start the server without blocking
            await app.StartAsync($"http://0.0.0.0:{port}");
            Console.WriteLine($"Web server started and listening on http://0.0.0.0:{port}");

            // Wait for the application lifetime to end (keeps server running indefinitely)
            await app.WaitForShutdownAsync();
            Console.WriteLine("Web server is shutting down...");
        }

        private async Task StartWorkerAsync(Func<Task> workerTask, int intervall)
        {
            var dateTimeLastRun = default(DateTime);
            while (true)
            {
                if (DateTime.Now - dateTimeLastRun >= new TimeSpan(0, 0, 0, intervall))
                {
                    await workerTask.Invoke();
                    dateTimeLastRun = DateTime.Now;
                }
                await Task.Delay(100);
            }
        }

        private async Task StartBoilingPlate1WorkerAsync()
        {
            await StartWorkerAsync(() => _boilingPlate1Worker.Execute(), 3);
        }

        private async Task StartBoilingPlate2WorkerAsync()
        {
            await StartWorkerAsync(() => _boilingPlate2Worker.Execute(), 3);
        }
    }
}
