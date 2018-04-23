using Brewery.Core.Contracts.ServiceAdapter;
using Brewery.Core.Models;
using System.Threading.Tasks;

namespace Brewery.ServiceAdapter
{
    class MashService : IMashService
    {
        private readonly RequestHelper _requestHelper;

        public MashService(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public async Task<MashServiceStatus> GetStatus()
        {
            var t = await _requestHelper.SendRequest<Response<MashServiceStatus>>("/mashService/status", MethodTypes.GET);
            return t.Value;
        }

        public async Task MessageAcknowledged()
        {
            await _requestHelper.SendRequest("/mashService/messageAcknowledged", MethodTypes.PUT);
        }

        public async Task PauseMashProcess()
        {
            await _requestHelper.SendRequest("/mashService/pauseMashProcess", MethodTypes.PUT);
        }

        public async Task StartMashProcess()
        {
            await _requestHelper.SendRequest("/mashService/startMashProcess", MethodTypes.PUT);
        }

        public async Task StopMashProcess()
        {
            await _requestHelper.SendRequest("/mashService/stopMashProcess", MethodTypes.PUT);
        }
    }
}