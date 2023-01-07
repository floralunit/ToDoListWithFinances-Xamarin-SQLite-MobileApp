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
                if (menuItem.Text == "О программе") {
                    Shell.Current.CurrentItem.CurrentItem.Items.Add(new AboutPage());
                }
                else if (menuItem != null) {
                    Shell.Current.CurrentItem.CurrentItem.Items.Add(new ItemsPage(menuItem.Text));
                }
                Shell.Current.CurrentItem.CurrentItem.Items.RemoveAt(0);
                Shell.Current.FlyoutIsPresented = false;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
