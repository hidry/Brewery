using Brewery.Core.Models;
using System.Threading.Tasks;

namespace Brewery.Server.Core.Service
{
    public interface IMashService
    {
        Task Execute();
        MashServiceStatus GetStatus();
        void StopMashProcess();
        void PauseMashProcess();
        void StartMashProcess();
        void MessageAcknowledged();
    }
}