using Brewery.Core.Models;
using GalaSoft.MvvmLight.Ioc;
using System.Diagnostics;

namespace Brewery.Core
{
    public static class IocContainer
    {
        static IocContainer()
        {
            Register<MashServiceStatus>(); //todo: 
        }

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
            try
            {
                return SimpleIoc.Default.GetInstance<T>();
            }
            catch (System.Exception ex)
            {
                Debugger.Break();
                throw;
            }            
        }
    }
}