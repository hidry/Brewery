using Brewery.Core.Models;
using System.Threading.Tasks;

namespace Brewery.Core.Contracts.ServiceAdapter
{
    public interface IMashService
    {
        Task<MashServiceStatus> GetStatus();
        Task StopMashProcess();
        Task PauseMashProcess();
        Task StartMashProcess();
        Task MessageAcknowledged();
    }
}