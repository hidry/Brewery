using System.Linq;

namespace Brewery.Server.Core
{
    public static class Settings
    {
        static Settings()
        {
            // Piezo-Summer - gpio 21 (Pin# 40)
            PiezoGpio = GpioHeader.Pins.First(p => p.GpioNumber == 21);
            // Mixer 1 - gpio 12 (Pin# 32)
            MixerGpio = GpioHeader.Pins.First(p => p.GpioNumber == 12);
            // Heizplatte 1 - gpio 16 (Pin# 36)
            BoilingPlate1Gpio = GpioHeader.Pins.First(p => p.GpioNumber == 16);
            // Heizplatte 2 - gpio 20 (Pin# 38)
            BoilingPlate2Gpio = GpioHeader.Pins.First(p => p.GpioNumber == 20);
            // Temperatursensor 1 - 1
            TemperatureSensor1OneWireAddress = "28-FF-EE-6B-91-16-04-90";
            // Temperatursensor 2 - 2
            TemperatureSensor2OneWireAddress = "28-FF-FA-4C-90-16-05-F0";
        }

        public static Gpio BoilingPlate2Gpio { get; set; }
        public static string TemperatureSensor2OneWireAddress { get; set; }
        public static Gpio BoilingPlate1Gpio { get; set; }
        public static Gpio MixerGpio { get; set; }
        public static string TemperatureSensor1OneWireAddress { get; set; }
        public static Gpio PiezoGpio { get; set; }
    }
}