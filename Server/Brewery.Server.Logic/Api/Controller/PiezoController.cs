using Brewery.Core;
using Brewery.Server.Core.Api;
using Microsoft.AspNetCore.Mvc;

namespace Brewery.Server.Logic.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PiezoController : ControllerBase
    {
        private readonly IGpioModule _gpioModule;

        public PiezoController()
        {
            _gpioModule = IocContainer.GetInstance<IGpioModule>();
        }

        [HttpPut("power/{power}")]
        public IActionResult Power(bool power)
        {
            _gpioModule.Power(21, power);
            return Ok();
        }
    }
}
