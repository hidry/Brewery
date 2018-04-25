using Brewery.Core;
using Brewery.Server.Core;
using Brewery.Server.Core.Api;
using Brewery.Server.Core.Models;
using Brewery.Server.Core.Service;
using Brewery.Server.Logic.RaspberryPi.Api;
using Brewery.Server.Logic.Service;

namespace Brewery.Server.Logic
{
    public static class Bootstrapper
    {
        public static void SetUpServerLogic()
        {
            ServiceAdapter.Bootstrapper.SetUpServiceAdapter();
            IocContainer.Register<MashSteps>();
            IocContainer.Register<IServer, Server>();
            IocContainer.Register<IGpioModule, GpioModule>();
            IocContainer.Register<ITemperatureModule, TemperatureModule>();
            IocContainer.Register<IMashService, MashService>();            
        }
    }
}