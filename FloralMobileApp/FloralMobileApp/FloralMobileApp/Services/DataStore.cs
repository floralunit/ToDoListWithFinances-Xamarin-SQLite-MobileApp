using FloralMobileApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloralMobileApp.Services
{
    public class DataStore : IDataStore<Item>
    {
        SQLiteAsyncConnection db;
        public DataStore(string databasePath)
        {
            db = new SQLiteAsyncConnection(databasePath);
        }

        public async Task CreateTableItems()
        {
            await db.CreateTableAsync<Item>();
        }
        public async Task CreateTableExpenses()
        {
            await db.CreateTableAsync<Expense>();
        }
        public async Task<List<Item>> GetItemsAsync()
        {
            return await db.Table<Item>().ToListAsync();

        }
        public async Task<List<Expense>> GetExpensesAsync()
        {
            return await db.Table<Expense>().ToListAsync();

        }
        public async Task<Item> GetItemAsync(int id)
        {
            return await db.GetAsync<Item>(id);
        }
        public async Task<int> DeleteItemAsync(Item item)
        {
            return await db.DeleteAsync(item);
        }
        public async Task<int> AddOrUpdateItemAsync(Item item)
        {
            if (item.Id != 0)
            {
                await db.UpdateAsync(item);
                return item.Id;
            }
            else
            {
                return await db.InsertAsync(item);
            }
        }
        public async Task<int> UpdateExpense(Expense expense)
        {
            return await db.UpdateAsync(expense);
        }
        public async Task UpdateExpenses()
        {
            var dt = DateTime.Now;
            var month = dt.Month;
            var year = dt.Year;
            var expenses = await App.Db.GetExpensesAsync();
            if (!expenses.Where(x=> x.Month == month && x.Year == year).Any()) {
                int daysCount;
                if (new int[] { 4, 6, 9, 11 }.Contains(month))
                    daysCount = 30;
                else if (month == 2)
                    daysCount = 28;
                else
                    daysCount = 31;
                for (int i = 1; i <= daysCount; i++)
                {
                    var expense = new Expense
                    {
                        Day = i,
                        Month = month,
                        Year = year,
                        SpentValue = 0,
                        ExtraExpense = ""
                    };
                    await db.InsertAsync(expense);
                }
            }

        }
    }
}