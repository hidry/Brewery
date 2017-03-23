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
            this.InitializeComponent();
        }

        private void GoToUrl(object sender, RoutedEventArgs routedEventArgs)
        {
            WebView.Navigate(new Uri(UrlTextBox.Text));
        }
    }
}