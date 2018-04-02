using Brewery.Core.Contracts;
using Brewery.Core.Contracts.ServiceAdapter;
using Brewery.Core.Models;
using Brewery.ServiceAdapter;
using GalaSoft.MvvmLight.Ioc;

namespace Brewery.Logic
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            SetUpModules();
            SetupViewModels();
            SimpleIoc.Default.Register<IDevicesService, DevicesService>();
            SimpleIoc.Default.Register<IBoilingPlate1Service, BoilingPlate1Service>();
            SimpleIoc.Default.Register<IBoilingPlate2Service, BoilingPlate2Service>();
        }

        private static void SetupViewModels()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<BrewProcessViewModel>();
            SimpleIoc.Default.Register<StatusViewModel>();
            SimpleIoc.Default.Register<ManualHandlingViewModel>();
            SimpleIoc.Default.Register<BrewProcessSteps>();
        }

        private static void SetUpModules()
        {
            SimpleIoc.Default.Register<RequestHelper>();
            SimpleIoc.Default.Register<IPiezoService, PiezoService>();
            SimpleIoc.Default.Register<IMixerService, MixerService>();
            SimpleIoc.Default.Register<IBoilingPlate1Service, BoilingPlate1Service>();
            SimpleIoc.Default.Register<IBoilingPlate2Service, BoilingPlate2Service>();
            SimpleIoc.Default.Register<ITimer, Timer>();
            SimpleIoc.Default.Register<IBrewProcessModule, BrewProcessModule>();
            SimpleIoc.Default.Register<IManualHandlingModule, ManualHandlingModule>();
        }

        public static T GetInstance<T>()
        {
            return SimpleIoc.Default.GetInstance<T>();
        }

        public MainViewModel Main => GetInstance<MainViewModel>();
        
        public BrewProcessViewModel BrewProcess => GetInstance<BrewProcessViewModel>();

        public StatusViewModel Status => GetInstance<StatusViewModel>();

        public ManualHandlingViewModel ManualHandling => GetInstance<ManualHandlingViewModel>();
    }
}