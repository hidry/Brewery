using Brewery.Server.Core;
using Brewery.Server.Core.Api;
using Brewery.Server.Core.Models;
using Brewery.Server.Logic.RaspberryPi.Api;
using GalaSoft.MvvmLight.Ioc;

namespace Brewery.Server.Logic
{
    public class IocContainer
    {
        static IocContainer()
        {
            Register<MashSteps>();
            Register<IServer, Server>();
            Register<IGpioModule, GpioModule>();
            Register<ITemperatureModule, TemperatureModule>();
        }

        private static void Register<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface
        {
            SimpleIoc.Default.Register<TInterface, TClass>();
        }

        private static void Register<TClass>()
            where TClass : class
        {
            SimpleIoc.Default.Register<TClass>();
        }

        public static T GetInstance<T>()
        {
            return SimpleIoc.Default.GetInstance<T>();
        }
    }
}