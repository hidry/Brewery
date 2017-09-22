using Brewery.Core.Models;
using GalaSoft.MvvmLight;

namespace Brewery.Logic
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly Settings _settings;

        public SettingsViewModel(Settings settings)
        {
            _settings = settings;
            MixerGpio = settings.MixerGpio;
            TemperatureSensor1OneWireAddress = settings.TemperatureSensor1OneWireAddress;
            TemperatureSensor2OneWireAddress = settings.TemperatureSensor2OneWireAddress;
            BoilingPlate1Gpio = settings.BoilingPlate1Gpio;
            BoilingPlate2Gpio = settings.BoilingPlate2Gpio;
            PiezoGpio = settings.PiezoGpio;
        }
        
        private Gpio _mixerGpio;
        public Gpio MixerGpio
        {
            get { return _mixerGpio; }
            private set { Set(() => MixerGpio, ref _mixerGpio, value);
                _settings.MixerGpio = value;
            }
        }

        private string _temperatureSensor1OneWireAddressOneWireAddress;
        public string TemperatureSensor1OneWireAddress
        {
            get { return _temperatureSensor1OneWireAddressOneWireAddress; }
            private set { Set(() => TemperatureSensor1OneWireAddress, ref _temperatureSensor1OneWireAddressOneWireAddress, value);
                _settings.TemperatureSensor1OneWireAddress = value;
            }
        }
        
        private Gpio _boilingPlate1Gpio;
        public Gpio BoilingPlate1Gpio
        {
            get { return _boilingPlate1Gpio; }
            private set { Set(() => BoilingPlate1Gpio, ref _boilingPlate1Gpio, value);
                _settings.BoilingPlate1Gpio = value;
            }
        }

        private string _temperatureSensor2OneWireAddress;
        public string TemperatureSensor2OneWireAddress
        {
            get { return _temperatureSensor2OneWireAddress; }
            private set { Set(() => TemperatureSensor2OneWireAddress, ref _temperatureSensor2OneWireAddress, value);
                _settings.TemperatureSensor2OneWireAddress = value;
            }
        }

        private Gpio _boilingPlate2Gpio;
        public Gpio BoilingPlate2Gpio
        {
            get { return _boilingPlate2Gpio; }
            private set { Set(() => BoilingPlate2Gpio, ref _boilingPlate2Gpio, value);
                _settings.BoilingPlate2Gpio = value;
            }
        }

        private Gpio _piezoGpio;
        public Gpio PiezoGpio
        {
            get { return _piezoGpio; }
            private set { Set(() => PiezoGpio, ref _piezoGpio, value);
                _settings.PiezoGpio = value;
            }
        }
    }
}