using Brewery.Core.Contracts.ServiceAdapter;
using System.Threading.Tasks;

namespace Brewery.ServiceAdapter
{
    public class PiezoService : IPiezoService
    {
        private readonly RequestHelper _requestHelper;
        public PiezoService(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public async Task Power(bool on)
        {
            await _requestHelper.SendRequest($"/piezo/power/{on}", MethodTypes.PUT);
        }
    }
}