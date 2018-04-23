using Brewery.Core;
using Brewery.Core.Contracts;
using Brewery.ServiceAdapter;
using Brewery.UI.Core.Contracts;

namespace Brewery.UI.Logic
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            SetUpModules();
            SetupViewModels();
            IocContainer.Register<IDevicesService, DevicesService>();
        }

        private static void SetupViewModels()
        {
            IocContainer.Register<MainViewModel>();
            IocContainer.Register<BrewProcessViewModel>();
            IocContainer.Register<StatusViewModel>();
            IocContainer.Register<ManualHandlingViewModel>();
            IocContainer.Register<BrewProcessSteps>();
        }

        private static void SetUpModules()
        {
            Bootstrapper.SetUpServiceAdapter();
            IocContainer.Register<ITimer, Timer>();
            IocContainer.Register<IBrewProcessModule, BrewProcessModule>();
            IocContainer.Register<IManualHandlingModule, ManualHandlingModule>();
        }

        public static T GetInstance<T>()
        {
            return IocContainer.GetInstance<T>();
        }

        public MainViewModel Main => GetInstance<MainViewModel>();
        
        public BrewProcessViewModel BrewProcess => GetInstance<BrewProcessViewModel>();

        public StatusViewModel Status => GetInstance<StatusViewModel>();

        public ManualHandlingViewModel ManualHandling => GetInstance<ManualHandlingViewModel>();
    }
}