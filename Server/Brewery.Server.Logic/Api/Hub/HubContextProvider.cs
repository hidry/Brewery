using Microsoft.AspNetCore.SignalR;

namespace Brewery.Server.Logic.Api.Hub
{
    public static class HubContextProvider
    {
        public static IHubContext<BoilingPlate1Hub> BoilingPlate1HubContext { get; set; }
        public static IHubContext<BoilingPlate2Hub> BoilingPlate2HubContext { get; set; }
        public static IHubContext<MashStepsHub> MashStepsHubContext { get; set; }
    }
}
