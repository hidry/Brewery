using System.Threading.Tasks;

namespace Brewery.Server.Core
{
    public interface IServer
    {
        Task StartServerAsync();
    }
}