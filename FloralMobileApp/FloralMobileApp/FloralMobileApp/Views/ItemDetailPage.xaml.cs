using FloralMobileApp.Services;
using FloralMobileApp.ViewModels;
using Sextant;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FloralMobileApp.Views
{
    public partial class ItemDetailPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}