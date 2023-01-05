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
            DependencyService.Register<IMessageService, MessageService>();
            InitializeComponent();

            DependencyService.Register<ItemManager>();

            RxApp.DefaultExceptionHandler = new RxExceptionHandler();

            Instance.InitializeForms();

            Locator
               .CurrentMutable
               .RegisterConstant<IItemManager>(new ItemManager());

            Locator
               .CurrentMutable
               .RegisterNavigationView(() => new NavigationView(RxApp.MainThreadScheduler, RxApp.TaskpoolScheduler, ViewLocator.Current))
               .RegisterParameterViewStackService()
               .RegisterView<ItemsPage, ItemsViewModel>()
               .RegisterView<ItemDetailPage, ItemDetailViewModel>()
               .RegisterViewModel(() => new ItemsViewModel(Locator.Current.GetService<IParameterViewStackService>(), Locator.Current.GetService<IItemManager>()))
               .RegisterViewModel(() => new ItemDetailViewModel(Locator.Current.GetService<IParameterViewStackService>(), Locator.Current.GetService<IItemManager>()));

            Locator
                .Current
                .GetService<IParameterViewStackService>()
                .PushPage<ItemsViewModel>(null, true, false)
                .Subscribe();

            MainPage = new AppShell();

            //MainPage = Locator.Current.GetNavigationView("NavigationView");
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
