using Brewery.Core;
using Brewery.Server.Core.Api;
using Brewery.Server.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Brewery.Server.Logic.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoilingPlate2Controller : ControllerBase
    {
        private readonly IGpioModule _gpioModule;
        private readonly ITemperatureModule _temperatureModule;
        private readonly BoilingPlate2Model _boilingPlate2Model;

        public BoilingPlate2Controller()
        {
            _gpioModule = IocContainer.GetInstance<IGpioModule>();
            _temperatureModule = IocContainer.GetInstance<ITemperatureModule>();
            _boilingPlate2Model = IocContainer.GetInstance<BoilingPlate2Model>();
        }

        [HttpGet("powerStatus")]
        public IActionResult GetPowerStatus()
        {
            return Ok(_boilingPlate2Model.PowerStatus);
        }

        [HttpPut("power/{on}")]
        public IActionResult Power(bool on)
        {
            _boilingPlate2Model.PowerStatus = on;
            return Ok();
        }

        [HttpGet("getCurrentTemperature")]
        public IActionResult GetCurrentTemperature()
        {
            return Ok(GetCurrentTemperatureValue());
        }

        private double GetCurrentTemperatureValue()
        {
            return _temperatureModule.GetCurrenTemperature(Settings.TemperatureSensor2OneWireAddress);
        }

        [HttpPut("setTemperature/{temperature}")]
        public IActionResult SetTemperature(double temperature)
        {
            _boilingPlate2Model.Temperature = temperature;
            return Ok();
        }

        [HttpGet("getTemperature")]
        public IActionResult GetTemperature()
        {
            return Ok(_boilingPlate2Model.Temperature);
        }
    }
}
