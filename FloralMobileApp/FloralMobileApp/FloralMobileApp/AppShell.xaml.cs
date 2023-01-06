using FloralMobileApp.ViewModels;
using FloralMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace FloralMobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddEditItemPage), typeof(AddEditItemPage));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                var menuItem = sender as MenuItem;
                if (menuItem != null)
                {
                    await Shell.Current.GoToAsync($"{nameof(ItemsPage)}?{nameof(ItemsViewModel.MenuTitle)}={menuItem.Text}");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
