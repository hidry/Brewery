using Brewery.Core.Contracts;
using Brewery.Modules;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Brewery.ViewModels
{
    class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SetUpModules();
            SetupViewModels();
        }

        private static void SetupViewModels()
        {
            SimpleIoc.Default.Register<MainViewModel>();
        }

        private static void SetUpModules()
        {
            SimpleIoc.Default.Register<IDateTimeModule, DateTimeModule>();
            SimpleIoc.Default.Register<IMixerModule, MixerModule>();
            SimpleIoc.Default.Register<ITemperatureModule, ITemperatureModule>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
    }
}