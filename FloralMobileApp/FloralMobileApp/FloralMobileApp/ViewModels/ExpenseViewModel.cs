using System;
using System.Collections.ObjectModel;
using FloralMobileApp.Models;
using Xamarin.Forms;
using FloralMobileApp.Views;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace FloralMobileApp.ViewModels
{
    public class ExpenseViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<Expense> Expenses { get; }
        public string MonthYear { get; set; } = $"{DateTime.Now.ToString("MMMM")} {DateTime.Now.Year}";
        public string ResultText { get; set; } = $"Итог на {DateTime.Now.ToString("d MMMM")}: ";
        public string MonthLimitText
        {
            get => _monthLimitText;
            set
            {
                SetProperty(ref _monthLimitText, value);
            }
        }
        public string DayLimitText
        {
            get => _dayLimitText;
            set
            {
                SetProperty(ref _dayLimitText, value);
            }
        }
        public double MonthLimit
        {
            get => _monthLimit;
            set
            {
                SetProperty(ref _monthLimit, value);
                int daysCount;
                if (new int[] { 4, 6, 9, 11 }.Contains(DateTime.Now.Month))
                    daysCount = 30;
                else if (DateTime.Now.Month == 2)
                    daysCount = 28;
                else
                    daysCount = 31;
                DayLimit = MonthLimit / daysCount;
                MonthLimitText = $"Лимит на месяц: {Math.Round(MonthLimit, 2)} ₽";
                DayLimitText = $"Лимит в день: {Math.Round(DayLimit, 2)} ₽";
            }
        }
        public static double DayLimit { get; set; } = 0;
        public string CalcResult
        {
            get => _calcResult;
            set
            {
                SetProperty(ref _calcResult, value);
            }
        }

        private string _dayLimitText;
        private string _monthLimitText;
        private double _monthLimit;
        private string _calcResult;

        #endregion

        #region Commands 

        public Command LoadExpensesCommand { get; }
        public Command<Expense> SpentValueChangedCommand { get; }
        public Command EditMonthLimitCommand { get; }

        #endregion

        #region Constructors

        public ExpenseViewModel()
        {
            Title ="Расходы";
            Expenses = new ObservableCollection<Expense>();
            LoadExpensesCommand = new Command(async () => await ExecuteLoadExpensesCommand());
            SpentValueChangedCommand = new Command<Expense>(SpentValueChanged);
            EditMonthLimitCommand = new Command(async () => await EditMonthLimitAsync());
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
            DeserializeEditMonthLimit();
            await ExecuteLoadExpensesCommand();
        }
        private async void SpentValueChanged(Expense expense)
        {
            await App.Db.UpdateExpense(expense);
            MoneyCalc();
        }
        private async Task EditMonthLimitAsync()
        {
            string result = await App.Current.MainPage.DisplayPromptAsync("floral.helper", "Введите новый лимит на месяц:", initialValue: MonthLimit.ToString(), keyboard: Keyboard.Numeric);

            if (!string.IsNullOrEmpty(result)) {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string pathFull = System.IO.Path.Combine(path, "xdoc.xml");
                var doc = XDocument.Load(pathFull);
                XElement Params = doc.Element("Params");
                var limit = Params?.Attribute("MonthLimit");
                limit.Value = result;
                doc.Save(pathFull);
                DeserializeEditMonthLimit();
            }
        }
        private void DeserializeEditMonthLimit()
        {
            XElement Params = App.XmlDoc.Element("Params");
            MonthLimit = double.Parse(Params?.Attribute("MonthLimit").Value);
        }
        private void MoneyCalc()
        {
            var day = DateTime.Now.Day;
            var perfectMoneySpent = day * DayLimit;
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