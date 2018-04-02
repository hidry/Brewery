using Brewery.Server.Core;
using Brewery.Server.Logic.RaspberryPi;
using GalaSoft.MvvmLight.Ioc;

namespace Brewery.Server.Logic
{
    public class IocContainer
    {
        static IocContainer()
        {
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

        public static T GetInstance<T>()
        {
            return SimpleIoc.Default.GetInstance<T>();
        }
    }
}