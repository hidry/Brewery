using Brewery.Core.Models;

namespace Brewery.Core.Contracts
{
    public interface IMixerModule
    {
        MixerModel ToggleStatus();
    }
}