using FloralMobileApp.Services;
using FloralMobileApp.Views;
using System;
using Xamarin.Forms;
using System.IO;

namespace FloralMobileApp
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "floralHelper.db";
        public static DataStore _db;
        public static DataStore Db
        {
            get
            {
                if (_db == null)
                {
                    _db = new DataStore(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return _db;
            }
        }
        public App()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddEditItemPage), typeof(AddEditItemPage));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(ExpensesPage), typeof(ExpensesPage));

            DependencyService.Register<IMessageService, MessageService>();
            DependencyService.Register<DataStore>();

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
