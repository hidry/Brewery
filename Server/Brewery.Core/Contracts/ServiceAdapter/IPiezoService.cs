using System.Threading.Tasks;

namespace Brewery.Core.Contracts.ServiceAdapter
{
    public interface IPiezoService
    {
        Task Power(bool on);
    }
}