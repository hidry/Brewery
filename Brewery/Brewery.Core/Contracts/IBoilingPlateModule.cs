using Brewery.Core.Models;

namespace Brewery.Core.Contracts
{
    public interface IBoilingPlateModule
    {
        BoilingPlateModel PowerOn();
        BoilingPlateModel PowerOff();
    }
}