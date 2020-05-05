using MobilityDC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilityDC.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<Models.MenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<Models.MenuItem>
            {
                new Models.MenuItem { Id = MenuItemType.Home, Title="Home" },
                new Models.MenuItem { Id = MenuItemType.Bulk, Title="Bulk Pick" },
                new Models.MenuItem { Id = MenuItemType.Fine, Title="Fine Pick" },
                new Models.MenuItem { Id = MenuItemType.Store, Title="Store Pick" },
                new Models.MenuItem { Id = MenuItemType.Help, Title="Help" },
                new Models.MenuItem { Id = MenuItemType.SignOut, Title="Sign Out" }
             
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((Models.MenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}