using Brewery.Core.Contracts;
using GalaSoft.MvvmLight;

namespace Brewery.Logic
{
    public class StatusViewModel : ViewModelBase
    {
        private readonly ITemperature1Module _temperature1Module;
        private readonly ITemperature2Module _temperature2Module;
        private readonly ITemperatureControl1Module _temperatureControl1Module;
        private readonly ITemperatureControl2Module _temperatureControl2Module;

        public StatusViewModel(ITimer timer, ITemperature1Module temperature1Module, ITemperature2Module temperature2Module, ITemperatureControl1Module temperatureControl1Module, ITemperatureControl2Module temperatureControl2Module)
        {
            _temperature1Module = temperature1Module;
            _temperature2Module = temperature2Module;
            _temperatureControl1Module = temperatureControl1Module;
            _temperatureControl2Module = temperatureControl2Module;
                        
            timer.AddEvent((sender, o) => UpdateProperties());
        }

        private void UpdateProperties()
        {
            Temperature1 = _temperature1Module.GetCurrenTemperature().Temperature;
            Temperature2 = _temperature2Module.GetCurrenTemperature().Temperature;
            BoilingPlate1 = _temperatureControl1Module.GetStatus().Heating;
            BoilingPlate2 = _temperatureControl2Module.GetStatus().Heating;
        }

        private double _temperature1;
        public double Temperature1
        {
            get { return _temperature1; }
            private set
            {
                Set(() => Temperature1, ref _temperature1, value);
            }
        }

        private double _temperature2;
        public double Temperature2
        {
            get { return _temperature2; }
            private set
            {
                Set(() => Temperature2, ref _temperature2, value);
            }
        }

        private bool _boilingPlate1;
        public bool BoilingPlate1
        {
            get { return _boilingPlate1; }
            private set
            {
                Set(() => BoilingPlate1, ref _boilingPlate1, value);
            }
        }

        private bool _boilingPlate2;
        public bool BoilingPlate2
        {
            get { return _boilingPlate2; }
            private set
            {
                Set(() => BoilingPlate2, ref _boilingPlate2, value);
            }
        }
    }
}