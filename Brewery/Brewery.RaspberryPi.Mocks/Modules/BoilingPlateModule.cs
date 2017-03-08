using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.RaspberryPi.Modules
{
    public class BoilingPlateModule : IBoilingPlateModule
    {
        public BoilingPlateModel PowerOn()
        {
            return new BoilingPlateModel() { Status = true };
        }

        public BoilingPlateModel PowerOff()
        {
            return new BoilingPlateModel() { Status = false };
        }
    }
}