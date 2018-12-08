using System.Threading.Tasks;

namespace Brewery.Core.Contracts.ServiceAdapter
{
    public interface IBoilingPlate1Service
    {
        Task PowerOn();
        Task PowerOff();
        Task<double> GetCurrenTemperature();
        Task<bool> GetPowerStatus();
    }
}