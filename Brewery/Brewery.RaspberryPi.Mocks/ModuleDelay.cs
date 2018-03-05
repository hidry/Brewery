using System.Diagnostics;

namespace Brewery.RaspberryPi
{
    static class ModuleDelay
    {
        public static void Sleep()
        {
            for (var i = 0; i < 20000; i++)
                Debug.WriteLine(i);
        }
    }
}