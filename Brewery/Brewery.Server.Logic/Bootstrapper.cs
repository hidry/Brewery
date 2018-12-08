using Brewery.Core;
using Brewery.Server.Core;
using Brewery.Server.Core.Api;
using Brewery.Server.Core.Service;
using Brewery.Server.Logic.RaspberryPi.Api;
using Brewery.Server.Logic.Service;
using Brewery.Server.Core.Models;

namespace Brewery.Server.Logic
{
    public static class Bootstrapper
    {
        public static void SetUpServerLogic()
        {
            ServiceAdapter.Bootstrapper.SetUpServiceAdapter();
            IocContainer.Register<BoilingPlate2Model>();
            IocContainer.Register<MashSteps>();
            IocContainer.Register<IServer, Server>();
            IocContainer.Register<IGpioModule, GpioModule>();
            IocContainer.Register<ITemperatureModule, TemperatureModule>();
            IocContainer.Register<IBoilingPlate1Worker, BoilingPlate1Worker>();
            IocContainer.Register<IBoilingPlate2Worker, BoilingPlate2Worker>();
        }
    }
}