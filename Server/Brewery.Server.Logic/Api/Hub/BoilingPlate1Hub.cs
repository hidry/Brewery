using Brewery.Core;
using Brewery.Server.Core.Api;
using Brewery.Server.Core.Service;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Brewery.Server.Logic.Api.Hub
{
    public class BoilingPlate1Hub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IBoilingPlate1Worker _boilingPlate1Worker;
        private readonly IGpioModule _gpioModule;
        private readonly ITemperatureModule _temperatureModule;

        public BoilingPlate1Hub()
        {
            _gpioModule = IocContainer.GetInstance<IGpioModule>();
            _temperatureModule = IocContainer.GetInstance<ITemperatureModule>();
            _boilingPlate1Worker = IocContainer.GetInstance<IBoilingPlate1Worker>();
        }

        // Client can call these methods
        public async Task StartMashProcess()
        {
            _boilingPlate1Worker.StartMashProcess();
            await Clients.All.SendAsync("MashProcessStarted");
        }

        public async Task StopMashProcess()
        {
            _boilingPlate1Worker.StopMashProcess();
            await Clients.All.SendAsync("MashProcessStopped");
        }

        public async Task AcknowledgeMessage()
        {
            _boilingPlate1Worker.AcknowledgeMessage();
            await Clients.All.SendAsync("MessageAcknowledged");
        }

        public async Task<bool> GetPowerStatus()
        {
            return await Task.FromResult(_boilingPlate1Worker.GetPowerStatus());
        }

        public async Task<double> GetCurrentTemperature()
        {
            return await Task.FromResult(_temperatureModule.GetCurrenTemperature(Settings.TemperatureSensor1OneWireAddress));
        }

        // Server calls these methods to push updates to clients
        public static async Task BroadcastPowerStatus(IHubContext<BoilingPlate1Hub> hubContext, bool powerStatus)
        {
            await hubContext.Clients.All.SendAsync("PowerStatusUpdated", powerStatus);
        }

        public static async Task BroadcastCurrentTemperature(IHubContext<BoilingPlate1Hub> hubContext, double temperature)
        {
            await hubContext.Clients.All.SendAsync("CurrentTemperatureUpdated", temperature);
        }

        public static async Task BroadcastCurrentStep(IHubContext<BoilingPlate1Hub> hubContext, string step, int estimatedTime)
        {
            await hubContext.Clients.All.SendAsync("CurrentStepUpdated", new { Step = step, EstimatedTime = estimatedTime });
        }
    }
}
