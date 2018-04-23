using GalaSoft.MvvmLight.Ioc;

namespace Brewery.Core
{
    public static class IocContainer
    {        
        public static void Register<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface
        {
            SimpleIoc.Default.Register<TInterface, TClass>();
        }

        public static void Register<TClass>()
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