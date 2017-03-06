using Brewery.Core.Models;

namespace Brewery.Core.Contracts
{
    interface IMixerModule
    {
        MixerModel ToggleStatus();
    }
}