using FloralMobileApp.Services;
using FloralMobileApp.Views;
using System;
using Xamarin.Forms;
using System.IO;
using System.Xml.Linq;
using System.Linq;

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
                if (_db == null) {
                    _db = new DataStore(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return _db;
            }
        }
        public static XDocument _xdoc;
        public static XDocument XmlDoc
        {
            get
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string pathFull = System.IO.Path.Combine(path, "xdoc.xml");

                if (!File.Exists(pathFull)) {
                    XDocument doc = new XDocument(new XElement("Params", new XAttribute("MonthLimit", "15000")));
                    doc.Save(pathFull);
                    _xdoc = doc;
                }
                else {
                    _xdoc = XDocument.Load(pathFull);
                }
                return _xdoc;
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
