using Windows.ApplicationModel.Background;
using Brewery.Server.Logic;
using Brewery.Server.Core;
using Brewery.Core;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace Brewery.Server
{
    public sealed class StartupTask : IBackgroundTask
    {
        BackgroundTaskDeferral deferral;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();

            Bootstrapper.SetUpServerLogic();
            
            var server = IocContainer.GetInstance<IServer>();
            await server.StartServerAsync();
        }
    }
}