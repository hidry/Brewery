using Brewery.Server.Logic;
using Brewery.Server.Core;
using Brewery.Core;
using System;
using System.Threading.Tasks;

namespace Brewery.Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Brewery Server starting...");
            
            try
            {
                // Setup IoC container
                Bootstrapper.SetUpServerLogic();

                // Get server instance and start
                var server = IocContainer.GetInstance<IServer>();
                await server.StartServerAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting server: {ex}");
                throw;
            }
        }
    }
}
