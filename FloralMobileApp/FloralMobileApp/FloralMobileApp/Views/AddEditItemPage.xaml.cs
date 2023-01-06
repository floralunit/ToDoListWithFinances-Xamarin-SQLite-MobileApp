using FloralMobileApp.ViewModels;

namespace FloralMobileApp.Views
{
    public partial class AddEditItemPage
    {
        public AddEditItemPage()
        {
            InitializeComponent();
            BindingContext = new AddEditItemViewModel();
        }
    }
}