using Brewery.ServerMock;
using Brewery.Server.Core;
using Brewery.Core;
using System;
using System.Threading.Tasks;

namespace Brewery.ServerMock
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Brewery Mock Server starting...");

            try
            {
                // Setup IoC container with MOCK implementations
                BootstrapperMock.SetUpServerLogicMock();

                // Get server instance and start
                var server = IocContainer.GetInstance<IServer>();
                await server.StartServerAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting mock server: {ex}");
                throw;
            }
        }
    }
}
