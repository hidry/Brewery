using System.Threading.Tasks;
using Brewery.Core.Contracts.ServiceAdapter;

namespace Brewery.ServiceAdapter
{
    public class BoilingPlate1Service : IBoilingPlate1Service
    {
        private readonly RequestHelper _requestHelper;

        public BoilingPlate1Service(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public async Task<double> GetCurrenTemperature()
        {
            var t = await _requestHelper.SendRequest<Response<double>>("/boilingPlate1/getCurrentTemperature", MethodTypes.GET);
            return t.Value;
        }

        public async Task<bool> GetPowerStatus()
        {
            var t = await _requestHelper.SendRequest<Response<bool>>("/boilingPlate1/powerStatus", MethodTypes.GET);
            return t.Value;
        }
        
        public async Task PowerOff()
        {
            await _requestHelper.SendRequest<Response<bool>>($"/boilingPlate1/power/{false}", MethodTypes.PUT);
        }

        public async Task PowerOn()
        {
            await _requestHelper.SendRequest<Response<bool>>($"/boilingPlate1/power/{true}", MethodTypes.PUT);
        }
    }
}