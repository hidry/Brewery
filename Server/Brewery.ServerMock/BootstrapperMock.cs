using Brewery.Core;
using Brewery.Server.Core;
using Brewery.Server.Core.Api;
using Brewery.Server.Core.Service;
using Brewery.Server.Logic.RaspberryPiMock.Api;
using Brewery.Server.Logic.Service;
using Brewery.Server.Core.Models;

namespace Brewery.ServerMock
{
    public static class BootstrapperMock
    {
        public static void SetUpServerLogicMock()
        {
            // Setup service adapter
            ServiceAdapter.Bootstrapper.SetUpServiceAdapter();

            // Register core models and services
            IocContainer.Register<BoilingPlate2Model>();
            IocContainer.Register<MashSteps>();
            IocContainer.Register<IServer, Server.Logic.Server>();

            // Register MOCK implementations for hardware
            IocContainer.Register<IGpioModule, GpioModule>();
            IocContainer.Register<ITemperatureModule, TemperatureModule>();

            // Register workers
            IocContainer.Register<IBoilingPlate1Worker, BoilingPlate1Worker>();
            IocContainer.Register<IBoilingPlate2Worker, BoilingPlate2Worker>();
        }
    }
}
