using System.Linq;

namespace Brewery.Core.Models
{
    public class Settings
    {
        public Settings()
        {
            // Mixer 1 - gpio 12 (Pin# 32)
            MixerGpio = GpioHeader.Pins.First(p => p.GpioNumber == 12);
            // Temperatursensor 1 - 1
            TemperatureSensor1OneWireAddress = "28-FF-FA-4C-90-16-05-F0";
            // Heizplatte 1 - gpio 16 (Pin# 36)
            BoilingPlate1Gpio = GpioHeader.Pins.First(p => p.GpioNumber == 16);
            // Temperatursensor 2 - 2
            TemperatureSensor2OneWireAddress = "#2"; //todo: richtige Adresse
            // Heizplatte 2 - gpio 20 (Pin# 38)
            BoilingPlate2Gpio = GpioHeader.Pins.First(p => p.GpioNumber == 20);
        }

        public Gpio BoilingPlate2Gpio { get; set; }
        public string TemperatureSensor2OneWireAddress { get; set; }
        public Gpio BoilingPlate1Gpio { get; set; }
        public Gpio MixerGpio { get; set; }
        public string TemperatureSensor1OneWireAddress { get; set; }
    }
}