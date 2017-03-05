using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;
using Rinsen.IoT.OneWire;
using System.Diagnostics;
using System.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Brewery
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool _mixerActive = false;
        private GpioController _gpioController;
        private GpioPin _gpio12; //pin 32
        private OneWireDeviceHandler _handler;
        private IEnumerable<DS18B20> _devices;

        public MainPage()
        {
            InitializeComponent();

            RefreshDateTime(null, null);
            var timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            timer.Tick += RefreshDateTime;
            timer.Start();

            _gpioController = GpioController.GetDefault();

            if (_gpioController == null)
            {
                Debug.WriteLine("No gpio controller available.");
            }
            else
            {
                InitGpio12();
            }

            _handler = new OneWireDeviceHandler(false, false); //todo: Dispose
            _devices = _handler.OneWireDevices.GetDevices<DS18B20>();
            if (_devices == null || !_devices.Any())
            {
                Debug.WriteLine("No temperature sensor (DS18B20) available.");
            }
            else
            {
                var temperatureTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
                temperatureTimer.Tick += RefreshTemperature;
                temperatureTimer.Start();
            }
        }

        private void RefreshTemperature(object sender, object e)
        {
            var result = _devices.First().GetTemperature();
            TemperatureTextBlock.Text = result.ToString();
        }

        private void InitGpio12()
        {
            _gpio12 = _gpioController.OpenPin(12);
            _gpio12.Write(GpioPinValue.Low);
            _gpio12.SetDriveMode(GpioPinDriveMode.Output);
        }

        private void RefreshDateTime(object sender, object e)
        {
            DateTimeTextBlock.Text = DateTime.Now.ToString();
        }

        private void MixerHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mixerActive)
            {
                if (_gpio12 != null)
                {
                    _gpio12.Write(GpioPinValue.Low);
                }
                MixerTextBlock.Text = "Aus";
                MixerProgressRing.IsActive = false;
            }
            else
            {
                if (_gpio12 != null)
                {
                    _gpio12.Write(GpioPinValue.High);
                }
                MixerTextBlock.Text = "An";
                MixerProgressRing.IsActive = true;
            }
            _mixerActive = !_mixerActive;
        }
    }
}