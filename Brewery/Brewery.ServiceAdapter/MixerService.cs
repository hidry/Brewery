using Brewery.Core.Contracts.ServiceAdapter;
using System.Threading.Tasks;

namespace Brewery.ServiceAdapter
{
    public class MixerService : IMixerService
    {
        private readonly RequestHelper _requestHelper;

        public MixerService(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public async Task Power(bool on)
        {
            await _requestHelper.SendRequest("/mixer/true", MethodTypes.PUT);
        }
    }
}