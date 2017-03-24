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
            //osk (on-screen-keyboard): https://code.msdn.microsoft.com/UWP-Custom-On-Screen-8fd8415e
            this.InitializeComponent();
            Keyboard.RegisterTarget(UrlTextBox);
            Keyboard.Visibility = Visibility.Collapsed;
            WebView.Visibility = Visibility.Visible;
        }

        private void GoToUrl(object sender, RoutedEventArgs routedEventArgs)
        {
            WebView.Navigate(new Uri(UrlTextBox.Text));
        }

        private void TextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            Keyboard.Visibility = Visibility.Visible;
            WebView.Visibility = Visibility.Collapsed;
        }

        private void TextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            Keyboard.Visibility = Visibility.Collapsed;
            WebView.Visibility = Visibility.Visible;
        }
    }
}