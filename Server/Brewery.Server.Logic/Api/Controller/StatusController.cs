using Microsoft.AspNetCore.Mvc;

namespace Brewery.Server.Logic.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet("serverStatus")]
        public IActionResult GetStatus()
        {
            return Ok(new { Message = "Server is up and running..." });
        }
    }
}
