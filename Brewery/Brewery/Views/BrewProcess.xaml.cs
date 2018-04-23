using Windows.UI.Xaml.Controls;
using Brewery.UI.Logic;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Brewery.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class BrewProcess : Page
    {
        public BrewProcess()
        {
            this.InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
        
        public BrewProcessViewModel ViewModel => (BrewProcessViewModel)DataContext;
    }
}