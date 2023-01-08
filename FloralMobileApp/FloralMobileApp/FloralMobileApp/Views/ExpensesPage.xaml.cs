using FloralMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FloralMobileApp.Views
{
    public partial class ExpensesPage
    {
        ExpenseViewModel _viewModel;
        public ExpensesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ExpenseViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAppearingAsync();
        }
    }
}