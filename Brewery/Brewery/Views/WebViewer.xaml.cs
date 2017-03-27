using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Brewery.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class WebViewer : Page
    {
        public WebViewer()
        {
            //todo: webview mit osk http://stackoverflow.com/questions/38502453/windows-10-iot-core-virtual-keyboard
            //osk (on-screen-keyboard): https://code.msdn.microsoft.com/UWP-Custom-On-Screen-8fd8415e
            this.InitializeComponent();
            Keyboard.RegisterTarget(UrlTextBox);
            //Keyboard.RegisterTarget(WebView);
            Keyboard.Visibility = Visibility.Collapsed;
        }

        private void GoToUrl(object sender, RoutedEventArgs routedEventArgs)
        {
            WebView.Navigate(new Uri(UrlTextBox.Text));
        }
        
        private void SwitchKeyboardVisibility(object sender, RoutedEventArgs e)
        {
            Keyboard.Visibility = Keyboard.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}