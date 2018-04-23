using Brewery.Core;
using Brewery.Core.Contracts.ServiceAdapter;

namespace Brewery.ServiceAdapter
{
    public static class Bootstrapper
    {
        public static void SetUpServiceAdapter()
        {
            IocContainer.Register<RequestHelper>();
            IocContainer.Register<IPiezoService, PiezoService>();
            IocContainer.Register<IMixerService, MixerService>();
            IocContainer.Register<IBoilingPlate1Service, BoilingPlate1Service>();
            IocContainer.Register<IBoilingPlate2Service, BoilingPlate2Service>();
            IocContainer.Register<IMashService, MashService>();
        }
    }
}