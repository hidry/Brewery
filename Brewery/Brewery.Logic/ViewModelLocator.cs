using Brewery.Core.Contracts;
using Brewery.Core.Models;
using Brewery.RaspberryPi.Modules;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Brewery.Logic
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SetUpModules();
            SetupViewModels();
            SimpleIoc.Default.Register<Settings>();
        }

        private static void SetupViewModels()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<BrewProcessViewModel>();
            SimpleIoc.Default.Register<StatusViewModel>();
            SimpleIoc.Default.Register<ManualHandlingViewModel>();
            SimpleIoc.Default.Register<BrewProcessSteps>();
        }

        private static void SetUpModules()
        {
            SimpleIoc.Default.Register<IPiezoModule, PiezoModule>();
            SimpleIoc.Default.Register<IMixerModule, MixerModule>();
            SimpleIoc.Default.Register<ITemperature1Module, Temperature1Module>();
            SimpleIoc.Default.Register<ITemperature2Module, Temperature2Module>();
            SimpleIoc.Default.Register<IBoilingPlate1Module, BoilingPlate1Module>();
            SimpleIoc.Default.Register<IBoilingPlate2Module, BoilingPlate2Module>();
            SimpleIoc.Default.Register<ITemperatureControl1Module, TemperatureControl1Module>();
            SimpleIoc.Default.Register<ITemperatureControl2Module, TemperatureControl2Module>();
            SimpleIoc.Default.Register<ITimer, Timer>();
            SimpleIoc.Default.Register<IBrewProcessModule, BrewProcessModule>();
            SimpleIoc.Default.Register<IManualHandlingModule, ManualHandlingModule>();
        }

        public static void DisposeCreatedInstances()
        {
            var createdTemperatureModules = SimpleIoc.Default.GetAllCreatedInstances<ITemperatureModule>();
            foreach (var instance in createdTemperatureModules)
            {
                instance.Dispose();
            }
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public SettingsViewModel SettingsView => ServiceLocator.Current.GetInstance<SettingsViewModel>();

        public BrewProcessViewModel BrewProcess => ServiceLocator.Current.GetInstance<BrewProcessViewModel>();

        public StatusViewModel Status => ServiceLocator.Current.GetInstance<StatusViewModel>();

        public ManualHandlingViewModel ManualHandling => ServiceLocator.Current.GetInstance<ManualHandlingViewModel>();
    }
}