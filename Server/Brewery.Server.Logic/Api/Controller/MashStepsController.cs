using Brewery.Core;
using Brewery.Server.Core.Models;
using Brewery.Server.Core.Service;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using System;
using System.Linq;

namespace Brewery.Server.Logic.Api.Controller
{
    [RestController(InstanceCreationType.Singleton)]
    class MashStepsController
    {
        private MashSteps _mashSteps { get; }
        private IBoilingPlate1Worker _boilingPlate1Worker { get; }

        public MashStepsController()
        {
            _mashSteps = IocContainer.GetInstance<MashSteps>();
            _boilingPlate1Worker = IocContainer.GetInstance<IBoilingPlate1Worker>();
        }
        
        [UriFormat("/mashSteps")]
        public IGetResponse GetMashSteps()
        {
            return new GetResponse(GetResponse.ResponseStatus.OK, _mashSteps.ToArray());
        }

        [UriFormat("/mashSteps/currentStep")]
        public IGetResponse GetCurrentMashStep()
        {
            return new GetResponse(GetResponse.ResponseStatus.OK, _boilingPlate1Worker.GetCurrentStep());
        }

        [UriFormat("/mashSteps/totalEstimatedRemainingTime")]
        public IGetResponse GetTotalEstimatedRemainingTime()
        {
            var totalTime = _mashSteps.Sum(ms => ms.Elapsed.TotalMinutes > ms.Rast ? ms.Elapsed.TotalMinutes : ms.Rast);
            var elapsedTime = _mashSteps.Sum(ms => ms.Elapsed.TotalMinutes);
            return new GetResponse(GetResponse.ResponseStatus.OK, totalTime - elapsedTime);
        }

        [UriFormat("/mashSteps")]
        public IPutResponse Update([FromContent] MashStep mashStep)
        {
            var index = _mashSteps.IndexOf(_mashSteps.First(ms => ms.Guid == mashStep.Guid));
            _mashSteps[index] = mashStep;
            return new PutResponse(PutResponse.ResponseStatus.OK, mashStep);
        }

        [UriFormat("/mashSteps/{guid}")]
        public IDeleteResponse Delete(string guid)
        {
            _mashSteps.Remove(_mashSteps.First(ms => ms.Guid == guid));
            return new DeleteResponse(DeleteResponse.ResponseStatus.OK);
        }

        [UriFormat("/mashSteps")]
        public IPostResponse Insert([FromContent] MashStep mashStep)
        {
            mashStep.Guid = Guid.NewGuid().ToString();
            _mashSteps.Add(mashStep);
            return new PostResponse(PostResponse.ResponseStatus.Created, null, mashStep);
        }
    }
}
