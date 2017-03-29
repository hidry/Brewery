using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Brewery.Core.Models;
using Brewery.Logic;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Brewery
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainViewModel ViewModel => (MainViewModel)DataContext;

        public MainPage()
        {
            InitializeComponent();

            HamburgerMenuControl.ItemsSource = MenuItem.GetMainItems();
            HamburgerMenuControl.OptionsItemsSource = MenuItem.GetOptionsItems();

            ContentFrame.Navigate(typeof(Views.BrewProcess));
            StatusFrame.Navigate(typeof(Views.Status));
        }

        private void OnMenuItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as MenuItem;
            if (menuItem != null) ContentFrame.Navigate(menuItem.PageType);
        }

        public RelayCommand CloseApplicationCommand => new RelayCommand(CloseApplication);

        private void CloseApplication()
        {
            Messenger.Default.Send(new ShowMessageDialog()
            {
                Title = "Schließen",
                Message = "Soll die App geschlossen werden?",
                OkButtonCommand = () =>
                {
                    Windows.UI.Xaml.Application.Current.Exit();
                },
                CancelButtonCommand = () => {}
            });
        }
    }

    public class MenuItem
    {
        public Symbol Icon { get; private set; }
        public string Name { get; private set; }
        public Type PageType { get; private set; }

        public static List<MenuItem> GetMainItems()
        {
            var items = new List<MenuItem>
            {
                new MenuItem() {Icon = Symbol.Bold, Name = "Brauprozess", PageType = typeof(Views.BrewProcess)},
                new MenuItem() {Icon = Symbol.Manage, Name = "Manuell", PageType = typeof(Views.ManualHandling)},
                new MenuItem() {Icon = Symbol.Globe, Name = "Internet", PageType = typeof(Views.WebViewer)},
                new MenuItem() {Icon = Symbol.Setting, Name = "Einstellungen", PageType = typeof(Views.Settings)}
            };
            return items;
        }

        public static List<MenuItem> GetOptionsItems()
        {
            var items = new List<MenuItem>();
            //items.Add(new MenuItem() { Icon = Symbol.Setting, Name = "Einstellungen", PageType = typeof(Views.SettingsView) });
            return items;
        }
    }
}