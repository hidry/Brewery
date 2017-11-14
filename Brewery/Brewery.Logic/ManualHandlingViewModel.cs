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
            TemperatureControl1Temperature = 77;
            TemperatureControl1OnOffSymbol = Symbol.Play;
            TemperatureControl2Temperature = 77;
            TemperatureControl2OnOffSymbol = Symbol.Play;
            MixerControlOnOffSymbol = Symbol.Play;
        }

        private int _temperatureControl1Temperature;
        public int TemperatureControl1Temperature
        {
            get => _temperatureControl1Temperature;
            private set
            {
                Set(() => TemperatureControl1Temperature, ref _temperatureControl1Temperature, value);
            }
        }

        public RelayCommand TemperatureControl1TemperatureDownCommand => new RelayCommand(TemperatureControl1TemperatureDown);

        private void TemperatureControl1TemperatureDown()
        {
            TemperatureControl1Temperature -= 1;
            _manualHandlingModule.ChangeTemperature1(-1);
        }

        public RelayCommand TemperatureControl1TemperatureUpCommand => new RelayCommand(TemperatureControl1TemperatureUp);

        private void TemperatureControl1TemperatureUp()
        {
            TemperatureControl1Temperature += 1;
            _manualHandlingModule.ChangeTemperature1(+1);
        }

        public RelayCommand TemperatureControl1OnOffCommand => new RelayCommand(TemperatureControl1OnOff);

        private void TemperatureControl1OnOff()
        {
            if (TemperatureControl1OnOffSymbol == Symbol.Play)
            {
                TemperatureControl1OnOffSymbol = Symbol.Stop;
                _manualHandlingModule.StartTemperatureControl1(TemperatureControl1Temperature);
            }
            else
            {
                TemperatureControl1OnOffSymbol = Symbol.Play;
                _manualHandlingModule.StopTemperatureControl1();
            }
        }
        
        private Symbol _temperatureControl1OnOffSymbol;
        public Symbol TemperatureControl1OnOffSymbol
        {
            get => _temperatureControl1OnOffSymbol;
            private set
            {
                Set(() => TemperatureControl1OnOffSymbol, ref _temperatureControl1OnOffSymbol, value);
            }
        }

        private int _temperatureControl2Temperature;
        public int TemperatureControl2Temperature
        {
            get => _temperatureControl2Temperature;
            private set
            {
                Set(() => TemperatureControl2Temperature, ref _temperatureControl2Temperature, value);
            }
        }

        public RelayCommand TemperatureControl2TemperatureDownCommand => new RelayCommand(TemperatureControl2TemperatureDown);

        private void TemperatureControl2TemperatureDown()
        {
            TemperatureControl2Temperature -= 1;
            _manualHandlingModule.ChangeTemperature2(-1);
        }

        public RelayCommand TemperatureControl2TemperatureUpCommand => new RelayCommand(TemperatureControl2TemperatureUp);

        private void TemperatureControl2TemperatureUp()
        {
            TemperatureControl2Temperature += 1;
            _manualHandlingModule.ChangeTemperature2(+1);
        }

        public RelayCommand TemperatureControl2OnOffCommand => new RelayCommand(TemperatureControl2OnOff);

        private void TemperatureControl2OnOff()
        {
            if (TemperatureControl2OnOffSymbol == Symbol.Play)
            {
                TemperatureControl2OnOffSymbol = Symbol.Stop;
                _manualHandlingModule.StartTemperatureControl2(TemperatureControl2Temperature);
            }
            else
            {
                TemperatureControl2OnOffSymbol = Symbol.Play;
                _manualHandlingModule.StopTemperatureControl2();
            }
        }

        private Symbol _temperatureControl2OnOffSymbol;
        public Symbol TemperatureControl2OnOffSymbol
        {
            get => _temperatureControl2OnOffSymbol;
            private set
            {
                Set(() => TemperatureControl2OnOffSymbol, ref _temperatureControl2OnOffSymbol, value);
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