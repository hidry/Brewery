using Brewery.Core;
using Brewery.Server.Core.Models;
using Brewery.Server.Core.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Brewery.Server.Logic.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MashStepsController : ControllerBase
    {
        private MashSteps _mashSteps { get; }
        private IBoilingPlate1Worker _boilingPlate1Worker { get; }

        public MashStepsController()
        {
            _mashSteps = IocContainer.GetInstance<MashSteps>();
            _boilingPlate1Worker = IocContainer.GetInstance<IBoilingPlate1Worker>();
        }

        [HttpGet]
        public IActionResult GetMashSteps()
        {
            return Ok(_mashSteps.ToArray());
        }

        [HttpGet("currentStep")]
        public IActionResult GetCurrentMashStep()
        {
            return Ok(_boilingPlate1Worker.GetCurrentStep());
        }

        [HttpGet("totalEstimatedRemainingTime")]
        public IActionResult GetTotalEstimatedRemainingTime()
        {
            return Ok(_mashSteps.Sum(ms => ms.EstimatedTime));
        }

        [HttpPut]
        public IActionResult Update([FromBody] MashStep mashStep)
        {
            var index = _mashSteps.IndexOf(_mashSteps.First(ms => ms.Guid == mashStep.Guid));
            _mashSteps[index] = mashStep;
            return Ok(mashStep);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(string guid)
        {
            _mashSteps.Remove(_mashSteps.First(ms => ms.Guid == guid));
            return Ok();
        }

        [HttpPost]
        public IActionResult Insert([FromBody] MashStep mashStep)
        {
            mashStep.Guid = Guid.NewGuid().ToString();
            _mashSteps.Add(mashStep);
            return CreatedAtAction(nameof(GetMashSteps), null, mashStep);
        }
    }
}
