using System.Threading.Tasks;

namespace Brewery.Core.Contracts.ServiceAdapter
{
    public interface IMixerService
    {
        Task Power(bool on);
    }
}