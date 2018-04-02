using System.Collections.Generic;

namespace Brewery.Server.Core
{
    public static class GpioHeader
    {
        public static List<Gpio> Pins => new List<Gpio>()
        {
            new Gpio() {Description = "GPIO", GpioNumber = 12, PinNumber = 32},
            new Gpio() {Description = "GPIO", GpioNumber = 16, PinNumber = 36},
            new Gpio() {Description = "GPIO", GpioNumber = 20, PinNumber = 38},
            new Gpio() {Description = "GPIO", GpioNumber = 21, PinNumber = 40}
        };
    }

    public class Gpio
    {
        public string Description { get; set; }
        public int GpioNumber { get; set; }
        public int PinNumber { get; set; }
        public override string ToString()
        {
            return $"{Description} {GpioNumber} (Pin# {PinNumber})";
        }
    }
}