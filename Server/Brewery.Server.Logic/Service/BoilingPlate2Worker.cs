using System.Diagnostics;
using System.Threading.Tasks;
using Brewery.Core.Contracts.ServiceAdapter;
using Brewery.Server.Core.Api;
using Brewery.Server.Core.Models;
using Brewery.Server.Logic.Api.Hubs;

namespace Brewery.Server.Logic.Service
{
    public class BoilingPlate2Worker : Core.Service.IBoilingPlate2Worker
    {
        private readonly IGpioModule _gpioModule;
        private readonly ITemperatureModule _temperatureModule;
        private readonly BoilingPlate2Model _boilingPlate2Model;
        private readonly IBoilingPlate2Service _boilingPlate2Service;

        public BoilingPlate2Worker(IGpioModule gpioModule, ITemperatureModule temperatureModule, BoilingPlate2Model boilingPlate2Model, IBoilingPlate2Service boilingPlate2Service)
        {
            _gpioModule = gpioModule;
            _temperatureModule = temperatureModule;
            _boilingPlate2Model = boilingPlate2Model;
            _boilingPlate2Service = boilingPlate2Service;
        }
        
        public async Task Execute()
        {
            try
            {
                if (!_boilingPlate2Model.PowerStatus)
                {
                    _gpioModule.Power(Settings.BoilingPlate2Gpio.GpioNumber, false);
                    return;
                }

                var temperatureCurrent = await _boilingPlate2Service.GetCurrenTemperature();
                if (temperatureCurrent < _boilingPlate2Model.Temperature)
                {
                    _gpioModule.Power(Settings.BoilingPlate2Gpio.GpioNumber, true);
                }
                else
                {
                    _gpioModule.Power(Settings.BoilingPlate2Gpio.GpioNumber, false);
                }

                // Broadcast updates via SignalR
                var hubContext = HubContextProvider.BoilingPlate2HubContext;
                if (hubContext != null)
                {
                    await BoilingPlate2Hub.BroadcastPowerStatus(hubContext, _boilingPlate2Model.PowerStatus);
                    await BoilingPlate2Hub.BroadcastCurrentTemperature(hubContext, temperatureCurrent);
                    await BoilingPlate2Hub.BroadcastTemperatureSetpoint(hubContext, _boilingPlate2Model.Temperature);
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }
    }
}