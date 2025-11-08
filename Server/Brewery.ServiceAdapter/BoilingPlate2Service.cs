using System.Threading.Tasks;
using Brewery.Core.Contracts.ServiceAdapter;

namespace Brewery.ServiceAdapter
{
    public class BoilingPlate2Service : IBoilingPlate2Service
    {
        private readonly RequestHelper _requestHelper;

        public BoilingPlate2Service(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public async Task<double> GetCurrenTemperature()
        {
            var t = await _requestHelper.SendRequest<double>("boilingPlate2/getCurrentTemperature", MethodTypes.GET);
            return t;
        }

        public async Task<bool> GetPowerStatus()
        {
            var t = await _requestHelper.SendRequest<bool>("boilingPlate2/powerStatus", MethodTypes.GET);
            return t;
        }

        public async Task PowerOff()
        {
            await _requestHelper.SendRequest<bool>($"boilingPlate2/power/{false}", MethodTypes.PUT);
        }

        public async Task PowerOn()
        {
            await _requestHelper.SendRequest<bool>($"boilingPlate2/power/{true}", MethodTypes.PUT);
        }
    }
}