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
            var t = await _requestHelper.SendRequest<double>("/boilingPlate1/getCurrentTemperature", MethodTypes.GET);
            return t;
        }

        public async Task<bool> GetPowerStatus()
        {
            var t = await _requestHelper.SendRequest<bool>("/boilingPlate1/powerStatus", MethodTypes.GET);
            return t;
        }
    }
}