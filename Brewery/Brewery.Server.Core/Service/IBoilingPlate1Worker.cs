using Brewery.Server.Core.Models;
using System.Threading.Tasks;

namespace Brewery.Server.Core.Service
{
    public interface IBoilingPlate1Worker
    {
        Task Execute();
        void StopMashProcess();
        void PauseMashProcess();
        void StartMashProcess();
        void MessageAcknowledged();
        MashStep GetCurrentStep();
        bool GetPowerStatus();
    }
}