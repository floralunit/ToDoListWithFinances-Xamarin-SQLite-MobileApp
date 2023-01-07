using FloralMobileApp.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FloralMobileApp.Views
{
    public partial class AddEditItemPage
    {
        public AddEditItemPage()
        {
            InitializeComponent();
            BindingContext = new AddEditItemViewModel();
        }
        protected async override void OnAppearing()
        {
            await Task.Run(async () =>
            {
                await Task.Delay(100);
                Device.BeginInvokeOnMainThread(() =>
                {
                    entry.Focus();
                });
            });
        }
    }
}