using Brewery.Core;
using Brewery.Server.Core.Api;
using Brewery.Server.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace Brewery.Server.Logic.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoilingPlate1Controller : ControllerBase
    {
        private readonly IBoilingPlate1Worker _boilingPlate1Worker;
        private readonly IGpioModule _gpioModule;
        private readonly ITemperatureModule _temperatureModule;

        public BoilingPlate1Controller()
        {
            _gpioModule = IocContainer.GetInstance<IGpioModule>();
            _temperatureModule = IocContainer.GetInstance<ITemperatureModule>();
            _boilingPlate1Worker = IocContainer.GetInstance<IBoilingPlate1Worker>();
        }

        [HttpGet("powerStatus")]
        public IActionResult GetPowerStatus()
        {
            return Ok(_boilingPlate1Worker.GetPowerStatus());
        }

        [HttpGet("getCurrentTemperature")]
        public IActionResult GetCurrentTemperature()
        {
            return Ok(GetTemperature());
        }

        private double GetTemperature()
        {
            return _temperatureModule.GetCurrenTemperature(Settings.TemperatureSensor1OneWireAddress);
        }

        [HttpPut("acknowledgeMessage")]
        public IActionResult AcknowledgeMessage()
        {
            _boilingPlate1Worker.AcknowledgeMessage();
            return Ok();
        }

        [HttpPut("startMashProcess")]
        public IActionResult StartMashProcess()
        {
            _boilingPlate1Worker.StartMashProcess();
            return Ok();
        }

        [HttpPut("stopMashProcess")]
        public IActionResult StopMashProcess()
        {
            _boilingPlate1Worker.StopMashProcess();
            return Ok();
        }
    }
}
