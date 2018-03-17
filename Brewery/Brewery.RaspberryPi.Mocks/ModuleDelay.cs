using System;

namespace Brewery.RaspberryPi
{
    static class ModuleDelay
    {
        public static void Sleep(string print = null)
        {
            var dateTimeStart = DateTime.Now;
            while ((DateTime.Now - dateTimeStart).Milliseconds < 500)
                ;
        }
    }
}