using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Brewery.Core.Contracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Brewery.Logic
{
    public class ManualHandlingViewModel : ViewModelBase
    {
        private readonly ITemperature1Module _temperature1Module;
        private readonly ITemperatureControl1Module _temperatureControl1Module;
        //private readonly DispatcherTimer _temperatureControl1Timer;
        private readonly ITemperature2Module _temperature2Module;
        private readonly ITemperatureControl2Module _temperatureControl2Module;
        //private readonly DispatcherTimer _temperatureControl2Timer;
        private readonly ITimer _timer;

        public ManualHandlingViewModel(ITimer timer, ITemperature1Module temperature1Module, ITemperatureControl1Module temperatureControl1Module, ITemperature2Module temperature2Module, ITemperatureControl2Module temperatureControl2Module)
        {
            _timer = timer;

            _temperature1Module = temperature1Module;
            _temperatureControl1Module = temperatureControl1Module;

            TemperatureControl1Temperature = 50;
            TemperatureControl1OnOffSymbol = Symbol.Play;

            _temperature2Module = temperature2Module;
            _temperatureControl2Module = temperatureControl2Module;

            TemperatureControl2Temperature = 50;
            TemperatureControl2OnOffSymbol = Symbol.Play;

            //_temperatureControl1Timer = new DispatcherTimer() {Interval = new TimeSpan(0, 0, 0, 1)};
            //_temperatureControl1Timer.Tick += (sender, o) =>  _temperatureControl1Module.ManageTemperature(TemperatureControl1Temperature,
            //    _temperature1Module.GetCurrenTemperature().Temperature);

            //_temperatureControl2Timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 1) };
            //_temperatureControl2Timer.Tick += (sender, o) => _temperatureControl2Module.ManageTemperature(TemperatureControl2Temperature,
            //    _temperature2Module.GetCurrenTemperature().Temperature);
        }

        private void TemperatureControl1Tick()
        {
            if (TemperatureControl1OnOffSymbol == Symbol.Stop)
                _temperatureControl1Module.ManageTemperature(TemperatureControl1Temperature, _temperature1Module.GetCurrenTemperature().Temperature);
        }

        private void TemperatureControl2Tick()
        {
            if (TemperatureControl2OnOffSymbol == Symbol.Stop)
                _temperatureControl2Module.ManageTemperature(TemperatureControl2Temperature, _temperature2Module.GetCurrenTemperature().Temperature);
        }

        private int _temperatureControl1Temperature;
        public int TemperatureControl1Temperature
        {
            get { return _temperatureControl1Temperature; }
            private set
            {
                Set(() => TemperatureControl1Temperature, ref _temperatureControl1Temperature, value);
            }
        }

        public RelayCommand TemperatureControl1TemperatureDownCommand => new RelayCommand(TemperatureControl1TemperatureDown);

        private void TemperatureControl1TemperatureDown()
        {
            TemperatureControl1Temperature -= 1;
        }

        public RelayCommand TemperatureControl1TemperatureUpCommand => new RelayCommand(TemperatureControl1TemperatureUp);

        private void TemperatureControl1TemperatureUp()
        {
            TemperatureControl1Temperature += 1;
        }

        public RelayCommand TemperatureControl1OnOffCommand => new RelayCommand(TemperatureControl1OnOff);

        private void TemperatureControl1OnOff()
        {
            if (TemperatureControl1OnOffSymbol == Symbol.Play)
            {
                TemperatureControl1OnOffSymbol = Symbol.Stop;
                _timer.AddEvent(nameof(TemperatureControl1Tick), (o, e) => TemperatureControl1Tick());
            }
            else
            {
                TemperatureControl1OnOffSymbol = Symbol.Play;
                _timer.RemoveEvent(nameof(TemperatureControl1Tick), (o, e) => TemperatureControl1Tick());
                _temperatureControl1Module.BoilingPlateOff();
            }
        }
        
        private Symbol _temperatureControl1OnOffSymbol;
        public Symbol TemperatureControl1OnOffSymbol
        {
            get { return _temperatureControl1OnOffSymbol; }
            private set
            {
                Set(() => TemperatureControl1OnOffSymbol, ref _temperatureControl1OnOffSymbol, value);
            }
        }





        private int _temperatureControl2Temperature;
        public int TemperatureControl2Temperature
        {
            get { return _temperatureControl2Temperature; }
            private set
            {
                Set(() => TemperatureControl2Temperature, ref _temperatureControl2Temperature, value);
            }
        }

        public RelayCommand TemperatureControl2TemperatureDownCommand => new RelayCommand(TemperatureControl2TemperatureDown);

        private void TemperatureControl2TemperatureDown()
        {
            TemperatureControl2Temperature -= 1;
        }

        public RelayCommand TemperatureControl2TemperatureUpCommand => new RelayCommand(TemperatureControl2TemperatureUp);

        private void TemperatureControl2TemperatureUp()
        {
            TemperatureControl1Temperature += 1;
        }

        public RelayCommand TemperatureControl2OnOffCommand => new RelayCommand(TemperatureControl2OnOff);

        private void TemperatureControl2OnOff()
        {
            if (TemperatureControl2OnOffSymbol == Symbol.Play)
            {
                TemperatureControl2OnOffSymbol = Symbol.Stop;
                _timer.AddEvent(nameof(TemperatureControl2Tick), (o, e) => TemperatureControl2Tick());
            }
            else
            {
                TemperatureControl2OnOffSymbol = Symbol.Play;
                _timer.RemoveEvent(nameof(TemperatureControl2Tick), (o, e) => TemperatureControl2Tick());
                _temperatureControl2Module.BoilingPlateOff();
            }
        }

        private Symbol _temperatureControl2OnOffSymbol;
        public Symbol TemperatureControl2OnOffSymbol
        {
            get { return _temperatureControl2OnOffSymbol; }
            private set
            {
                Set(() => TemperatureControl2OnOffSymbol, ref _temperatureControl2OnOffSymbol, value);
            }
        }
    }
}