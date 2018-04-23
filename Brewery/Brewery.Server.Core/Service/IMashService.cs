using Brewery.Core.Models;

namespace Brewery.Server.Core.Service
{
    public interface IMashService
    {
        void Execute();
        MashServiceStatus GetStatus();
        void StopMashProcess();
        void PauseMashProcess();
        void StartMashProcess();
        void MessageAcknowledged();
    }
}