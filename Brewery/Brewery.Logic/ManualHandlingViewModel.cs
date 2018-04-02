using Windows.UI.Xaml.Controls;
using Brewery.Core.Contracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Brewery.Logic
{
    public class ManualHandlingViewModel : ViewModelBase
    {
        private readonly IManualHandlingModule _manualHandlingModule;

        public ManualHandlingViewModel(IManualHandlingModule manualHandlingModule)
        {
            _manualHandlingModule = manualHandlingModule;
            BoilingPlate1Temperature = 77;
            BoilingPlate1OnOffSymbol = Symbol.Play;
            BoilingPlate2Temperature = 77;
            BoilingPlate2OnOffSymbol = Symbol.Play;
            MixerControlOnOffSymbol = Symbol.Play;
        }

        private int _boilingPlate1Temperature;
        public int BoilingPlate1Temperature
        {
            get => _boilingPlate1Temperature;
            private set
            {
                Set(() => BoilingPlate1Temperature, ref _boilingPlate1Temperature, value);
            }
        }

        public RelayCommand BoilingPlate1TemperatureDownCommand => new RelayCommand(BoilingPlate1TemperatureDown);

        private void BoilingPlate1TemperatureDown()
        {
            BoilingPlate1Temperature -= 1;
            _manualHandlingModule.ChangeBoilingPlate1Temperature(-1);
        }

        public RelayCommand BoilingPlate1TemperatureUpCommand => new RelayCommand(BoilingPlate1TemperatureUp);

        private void BoilingPlate1TemperatureUp()
        {
            BoilingPlate1Temperature += 1;
            _manualHandlingModule.ChangeBoilingPlate1Temperature(+1);
        }

        public RelayCommand Boilingplate1TemperatureControlOnOffCommand => new RelayCommand(BoilingPlate1TemperatureControlOnOff);

        private void BoilingPlate1TemperatureControlOnOff()
        {
            if (BoilingPlate1OnOffSymbol == Symbol.Play)
            {
                BoilingPlate1OnOffSymbol = Symbol.Stop;
                _manualHandlingModule.StartBoilingPlate1TemperatureControl(BoilingPlate1Temperature);
            }
            else
            {
                BoilingPlate1OnOffSymbol = Symbol.Play;
                _manualHandlingModule.StopBoilingPlate1TemperatureControl();
            }
        }
        
        private Symbol _boilingPlate1OnOffSymbol;
        public Symbol BoilingPlate1OnOffSymbol
        {
            get => _boilingPlate1OnOffSymbol;
            private set
            {
                Set(() => BoilingPlate1OnOffSymbol, ref _boilingPlate1OnOffSymbol, value);
            }
        }

        private int _boilingPlate2Temperature;
        public int BoilingPlate2Temperature
        {
            get => _boilingPlate2Temperature;
            private set
            {
                Set(() => BoilingPlate2Temperature, ref _boilingPlate2Temperature, value);
            }
        }

        public RelayCommand BoilingPlate2TemperatureDownCommand => new RelayCommand(BoilingPlate2TemperatureDown);

        private void BoilingPlate2TemperatureDown()
        {
            BoilingPlate2Temperature -= 1;
            _manualHandlingModule.ChangeBoilingPlate2Temperature(-1);
        }

        public RelayCommand BoilingPlate2TemperatureUpCommand => new RelayCommand(BoilingPlate2TemperatureUp);

        private void BoilingPlate2TemperatureUp()
        {
            BoilingPlate2Temperature += 1;
            _manualHandlingModule.ChangeBoilingPlate2Temperature(+1);
        }

        public RelayCommand BoilingPlate2OnOffCommand => new RelayCommand(BoilingPlate2OnOff);

        private void BoilingPlate2OnOff()
        {
            if (BoilingPlate2OnOffSymbol == Symbol.Play)
            {
                BoilingPlate2OnOffSymbol = Symbol.Stop;
                _manualHandlingModule.StartBoilingPlate2TemperatureControl(BoilingPlate2Temperature);
            }
            else
            {
                BoilingPlate2OnOffSymbol = Symbol.Play;
                _manualHandlingModule.StopBoilingPlate2();
            }
        }

        private Symbol _boilingPlate2OnOffSymbol;
        public Symbol BoilingPlate2OnOffSymbol
        {
            get => _boilingPlate2OnOffSymbol;
            private set
            {
                Set(() => BoilingPlate2OnOffSymbol, ref _boilingPlate2OnOffSymbol, value);
            }
        }

        public RelayCommand MixerControlOnOffCommand => new RelayCommand(MixerControlOnOff);

        private void MixerControlOnOff()
        {
            if (MixerControlOnOffSymbol == Symbol.Play)
            {
                MixerControlOnOffSymbol = Symbol.Stop;
                _manualHandlingModule.StartMixerControl();
            }
            else
            {
                MixerControlOnOffSymbol = Symbol.Play;
                _manualHandlingModule.StopMixerControl();
            }
        }

        private Symbol _mixerControlOnOffSymbol;
        public Symbol MixerControlOnOffSymbol
        {
            get => _mixerControlOnOffSymbol;
            private set
            {
                Set(() => MixerControlOnOffSymbol, ref _mixerControlOnOffSymbol, value);
            }
        }

        public RelayCommand PiezoControlOnOffCommand => new RelayCommand(PiezoControlOnOff);

        private void PiezoControlOnOff()
        {
            if (PiezoControlOnOffSymbol == Symbol.Play)
            {
                PiezoControlOnOffSymbol = Symbol.Stop;
                _manualHandlingModule.StartPizeoControl();
            }
            else
            {
                PiezoControlOnOffSymbol = Symbol.Play;
                _manualHandlingModule.StopPizeoControl();
            }
        }

        private Symbol _piezoControlOnOffSymbol;
        public Symbol PiezoControlOnOffSymbol
        {
            get => _piezoControlOnOffSymbol;
            private set
            {
                Set(() => PiezoControlOnOffSymbol, ref _piezoControlOnOffSymbol, value);
            }
        }
    }
}