using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace Brewery.Core
{
    public static class IocContainer
    {
        private static ServiceCollection _services = new ServiceCollection();
        private static ServiceProvider _serviceProvider;
        private static bool _isBuilt = false;

        public static void Register<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface
        {
            if (_isBuilt)
            {
                throw new InvalidOperationException("Cannot register services after the container has been built.");
            }
            _services.AddSingleton<TInterface, TClass>();
        }

        public static void Register<TClass>()
            where TClass : class
        {
            if (_isBuilt)
            {
                throw new InvalidOperationException("Cannot register services after the container has been built.");
            }
            _services.AddSingleton<TClass>();
        }

        public static void Build()
        {
            if (!_isBuilt)
            {
                _serviceProvider = _services.BuildServiceProvider();
                _isBuilt = true;
            }
        }

        public static T GetInstance<T>()
        {
            try
            {
                if (!_isBuilt)
                {
                    Build();
                }
                return _serviceProvider.GetRequiredService<T>();
            }
            catch
            {
                Debugger.Break();
                throw;
            }
        }
    }
}