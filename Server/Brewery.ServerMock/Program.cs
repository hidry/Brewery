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
                // Determine port: command-line args > environment variable > default (8801)
                int port = GetPort(args, defaultPort: 8801);

                // Setup IoC container with MOCK implementations
                BootstrapperMock.SetUpServerLogicMock();

                // Get server instance and start
                var server = IocContainer.GetInstance<IServer>();
                await server.StartServerAsync(port);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting mock server: {ex}");
                throw;
            }
        }

        private static int GetPort(string[] args, int defaultPort)
        {
            // Check command-line arguments first
            if (args.Length > 0 && int.TryParse(args[0], out int portFromArgs))
            {
                Console.WriteLine($"Using port from command-line argument: {portFromArgs}");
                return portFromArgs;
            }

            // Check environment variable
            string portEnv = Environment.GetEnvironmentVariable("BREWERY_PORT");
            if (!string.IsNullOrEmpty(portEnv) && int.TryParse(portEnv, out int portFromEnv))
            {
                Console.WriteLine($"Using port from environment variable: {portFromEnv}");
                return portFromEnv;
            }

            // Use default port
            Console.WriteLine($"Using default port: {defaultPort}");
            return defaultPort;
        }
    }
}
