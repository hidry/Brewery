using Brewery.Core;
using Brewery.Server.Core.Models;
using Brewery.Server.Core.Service;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Brewery.Server.Logic.Api.Hub
{
    public class MashStepsHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private MashSteps _mashSteps { get; }
        private IBoilingPlate1Worker _boilingPlate1Worker { get; }

        public MashStepsHub()
        {
            _mashSteps = IocContainer.GetInstance<MashSteps>();
            _boilingPlate1Worker = IocContainer.GetInstance<IBoilingPlate1Worker>();
        }

        // Client can call these methods
        public async Task<MashStep[]> GetMashSteps()
        {
            return await Task.FromResult(_mashSteps.ToArray());
        }

        public async Task<MashStep> GetCurrentMashStep()
        {
            return await Task.FromResult(_boilingPlate1Worker.GetCurrentStep());
        }

        public async Task<int> GetTotalEstimatedRemainingTime()
        {
            return await Task.FromResult(_mashSteps.Sum(ms => ms.EstimatedTime));
        }

        public async Task UpdateMashStep(MashStep mashStep)
        {
            var index = _mashSteps.IndexOf(_mashSteps.First(ms => ms.Guid == mashStep.Guid));
            _mashSteps[index] = mashStep;
            await Clients.All.SendAsync("MashStepUpdated", mashStep);
            await Clients.All.SendAsync("MashStepsChanged");
        }

        public async Task DeleteMashStep(string guid)
        {
            _mashSteps.Remove(_mashSteps.First(ms => ms.Guid == guid));
            await Clients.All.SendAsync("MashStepDeleted", guid);
            await Clients.All.SendAsync("MashStepsChanged");
        }

        public async Task<MashStep> InsertMashStep(MashStep mashStep)
        {
            mashStep.Guid = Guid.NewGuid().ToString();
            _mashSteps.Add(mashStep);
            await Clients.All.SendAsync("MashStepInserted", mashStep);
            await Clients.All.SendAsync("MashStepsChanged");
            return mashStep;
        }

        // Server calls these methods to push updates to clients
        public static async Task BroadcastCurrentStep(IHubContext<MashStepsHub> hubContext, MashStep currentStep)
        {
            await hubContext.Clients.All.SendAsync("CurrentStepUpdated", currentStep);
        }

        public static async Task BroadcastTotalEstimatedRemainingTime(IHubContext<MashStepsHub> hubContext, int totalTime)
        {
            await hubContext.Clients.All.SendAsync("TotalEstimatedRemainingTimeUpdated", totalTime);
        }

        public static async Task BroadcastMashStepsChanged(IHubContext<MashStepsHub> hubContext)
        {
            await hubContext.Clients.All.SendAsync("MashStepsChanged");
        }
    }
}
