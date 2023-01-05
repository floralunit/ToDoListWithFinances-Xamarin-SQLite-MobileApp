using FloralMobileApp.Services;
using FloralMobileApp.ViewModels;
using Sextant;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FloralMobileApp.Views
{
    public partial class ItemsPage
    {
        ItemsViewModel _viewModel;
        public ItemsPage()
        {
            InitializeComponent();
            DisplayAlert("Уведомление", "Пришло новое сообщение во view", "ОK");
            BindingContext = _viewModel = new ItemsViewModel(Locator.Current.GetService<IParameterViewStackService>(), Locator.Current.GetService<IItemManager>());
        }
    }
}