using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace Brewery.RaspberryPi
{
    static class ModuleDelay
    {
        public static async void Sleep(string print = null)
        {
            await Task.Run(() =>
            {
                for (var i = 0; i < 1000000; i++)
                    //if (i == 0 || i == 10000 -1)
                    if (print != null)
                        Debug.WriteLine($"{print} {i}");
            });
        }
    }
}