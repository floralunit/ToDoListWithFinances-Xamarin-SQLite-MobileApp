using System;
using System.Collections.ObjectModel;
using FloralMobileApp.Models;
using Xamarin.Forms;
using FloralMobileApp.Views;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;

namespace FloralMobileApp.ViewModels
{
    public class ExpenseViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<Expense> Expenses { get; }
        public string MonthYear { get; set; } = $"{DateTime.Now.ToString("MMMM")} {DateTime.Now.Year}";
        public string ResultText { get; set; } = $"Итог на {DateTime.Now.ToString("d MMMM")}:";
        public string CalcResult
        {
            get => _calcResult;
            set
            {
                SetProperty(ref _calcResult, value);
            }
        }
        private string _calcResult;

        #endregion

        #region Commands 

        public Command LoadExpensesCommand { get; }
        public Command<Expense> SpentValueChangedCommand { get; }

        #endregion

        #region Constructors

        public ExpenseViewModel()
        {
            Title ="Расходы";
            Expenses = new ObservableCollection<Expense>();
            LoadExpensesCommand = new Command(async () => await ExecuteLoadExpensesCommand());
            SpentValueChangedCommand = new Command<Expense>(SpentValueChanged);
        }

        #endregion

        #region Command Handlers

        async Task ExecuteLoadExpensesCommand()
        {
            IsBusy = true;
            await App.Db.UpdateExpenses();

            try
            {
                Expenses.Clear();
                var expenses = await App.Db.GetExpensesAsync();
                foreach (var expense in expenses.Where(x => x.Month == DateTime.Now.Month && x.Year == DateTime.Now.Year))
                {
                    Expenses.Add(expense);
                }
                MoneyCalc();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async Task OnAppearingAsync()
        {
            await App.Db.CreateTableExpenses();
            IsBusy = true;
        }
        private async void SpentValueChanged(Expense expense)
        {
            await App.Db.UpdateExpense(expense);
            MoneyCalc();
        }

        private void MoneyCalc()
        {
            var day = DateTime.Now.Day;
            var perfectMoneySpent = day * 500;
            double realMoneySpent = 0;
            foreach (var ex in Expenses)
            {
                realMoneySpent += ex.SpentValue;
            }
            var difference = Math.Round(perfectMoneySpent - realMoneySpent, 2);
            if(difference > 0) {
                CalcResult = $"+ {difference}";
            }
            else {
                CalcResult = $"{difference}";
            }
        }

        #endregion

    }
}