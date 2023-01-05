using FloralMobileApp.Services;
using FloralMobileApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FloralMobileApp.ViewModels;
using ReactiveUI;
using Sextant;
using Sextant.XamForms;
using Splat;
using static Sextant.Sextant;

namespace FloralMobileApp
{
    public partial class App : Application
    {

        public App()
        {
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            DependencyService.Register<IMessageService, MessageService>();
            InitializeComponent();
            DependencyService.Register<MockDataStore>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
