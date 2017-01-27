using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;

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

        public MainPage()
        {
            this.InitializeComponent();
            
            RefreshDateTime(null, null);
            var timer = new DispatcherTimer() { Interval = new TimeSpan(0,0,1) };
            timer.Tick += RefreshDateTime;
            timer.Start();

            _gpioController = GpioController.GetDefault();

            if (_gpioController == null)
            {
                throw new Exception("There is no GPIO controller on this device.");
            }

            InitGpio12();
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
                _gpio12.Write(GpioPinValue.Low);
                MixerTextBlock.Text = "Aus";
            }
            else
            {
                _gpio12.Write(GpioPinValue.High);
                MixerTextBlock.Text = "An";
            }            
            _mixerActive = !_mixerActive;
        }
    }
}