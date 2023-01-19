using ReactiveUI;
using SQLite;
using System;

namespace FloralMobileApp.Models
{
    [Table("Expenses")]
    public class Expense : ReactiveObject
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public double SpentValue
        {
            get => _spentValue;
            set => this.RaiseAndSetIfChanged(ref _spentValue, value);
        }
        public string ExtraExpense
        {
            get => _extraExpense;
            set => this.RaiseAndSetIfChanged(ref _extraExpense, value);
        }
        private string _extraExpense;
        private double _spentValue;
    }
}