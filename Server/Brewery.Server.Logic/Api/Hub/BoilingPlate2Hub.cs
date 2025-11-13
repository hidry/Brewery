using Brewery.Core;
using Brewery.Server.Core.Api;
using Brewery.Server.Core.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Brewery.Server.Logic.Api.Hub
{
    public class BoilingPlate2Hub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IGpioModule _gpioModule;
        private readonly ITemperatureModule _temperatureModule;
        private readonly BoilingPlate2Model _boilingPlate2Model;

        public BoilingPlate2Hub()
        {
            _gpioModule = IocContainer.GetInstance<IGpioModule>();
            _temperatureModule = IocContainer.GetInstance<ITemperatureModule>();
            _boilingPlate2Model = IocContainer.GetInstance<BoilingPlate2Model>();
        }

        // Client can call these methods
        public async Task<bool> GetPowerStatus()
        {
            return await Task.FromResult(_boilingPlate2Model.PowerStatus);
        }

        public async Task SetPower(bool on)
        {
            _boilingPlate2Model.PowerStatus = on;
            await Clients.All.SendAsync("PowerStatusUpdated", on);
        }

        public async Task<double> GetCurrentTemperature()
        {
            return await Task.FromResult(_temperatureModule.GetCurrenTemperature(Settings.TemperatureSensor2OneWireAddress));
        }

        public async Task<double> GetTemperature()
        {
            return await Task.FromResult(_boilingPlate2Model.Temperature);
        }

        public async Task SetTemperature(double temperature)
        {
            _boilingPlate2Model.Temperature = temperature;
            await Clients.All.SendAsync("TemperatureSetpointUpdated", temperature);
        }

        // Server calls these methods to push updates to clients
        public static async Task BroadcastPowerStatus(IHubContext<BoilingPlate2Hub> hubContext, bool powerStatus)
        {
            await hubContext.Clients.All.SendAsync("PowerStatusUpdated", powerStatus);
        }

        public static async Task BroadcastCurrentTemperature(IHubContext<BoilingPlate2Hub> hubContext, double temperature)
        {
            await hubContext.Clients.All.SendAsync("CurrentTemperatureUpdated", temperature);
        }

        public static async Task BroadcastTemperatureSetpoint(IHubContext<BoilingPlate2Hub> hubContext, double temperature)
        {
            await hubContext.Clients.All.SendAsync("TemperatureSetpointUpdated", temperature);
        }
    }
}
