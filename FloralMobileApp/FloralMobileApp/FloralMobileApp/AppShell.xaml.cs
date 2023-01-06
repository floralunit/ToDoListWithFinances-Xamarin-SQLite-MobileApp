using FloralMobileApp.ViewModels;
using FloralMobileApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FloralMobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddEditItemPage), typeof(AddEditItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
