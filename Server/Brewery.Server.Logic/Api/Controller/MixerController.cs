using Brewery.Core;
using Brewery.Server.Core.Api;
using Microsoft.AspNetCore.Mvc;

namespace Brewery.Server.Logic.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MixerController : ControllerBase
    {
        private readonly IGpioModule _gpioModule;

        public MixerController()
        {
            _gpioModule = IocContainer.GetInstance<IGpioModule>();
        }

        [HttpPut("power/{power}")]
        public IActionResult Power(bool power)
        {
            _gpioModule.Power(12, power);
            return Ok();
        }
    }
}
