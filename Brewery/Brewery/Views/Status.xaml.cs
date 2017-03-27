using Windows.UI.Xaml.Controls;
using Brewery.Logic;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Brewery.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Status : Page
    {
        public Status()
        {
            this.InitializeComponent();
        }

        public StatusViewModel ViewModel => (StatusViewModel)DataContext;
    }
}