using Brewery.UI.Core.Contracts;
using GalaSoft.MvvmLight;

namespace Brewery.UI.Logic
{
    public class StatusViewModel : ViewModelBase
    {
        public StatusViewModel(IDevicesService devicesService)
        {
            devicesService.Temperature1ChangedEvent += (sender, args) => Temperature1 = args.Temperature;
            devicesService.Temperature2ChangedEvent += (sender, args) => Temperature2 = args.Temperature;
            devicesService.HeatingStatus1ChangedEvent += (sender, args) => BoilingPlate1 = args.Heating;
            devicesService.HeatingStatus2ChangedEvent += (sender, args) => BoilingPlate2 = args.Heating;
        }

        private double _temperature1;
        public double Temperature1
        {
            get => _temperature1;
            private set
            {
                Set(() => Temperature1, ref _temperature1, value);
            }
        }

        private double _temperature2;
        public double Temperature2
        {
            get => _temperature2;
            private set
            {
                Set(() => Temperature2, ref _temperature2, value);
            }
        }

        private bool _boilingPlate1;
        public bool BoilingPlate1
        {
            get => _boilingPlate1;
            private set
            {
                Set(() => BoilingPlate1, ref _boilingPlate1, value);
            }
        }

        private bool _boilingPlate2;
        public bool BoilingPlate2
        {
            get => _boilingPlate2;
            private set
            {
                Set(() => BoilingPlate2, ref _boilingPlate2, value);
            }
        }
    }
}