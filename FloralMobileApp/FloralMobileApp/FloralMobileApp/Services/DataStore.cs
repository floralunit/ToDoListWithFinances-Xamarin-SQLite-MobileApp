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

        public async Task CreateTable()
        {
            await db.CreateTableAsync<Item>();
        }
        public async Task<List<Item>> GetItemsAsync()
        {
            return await db.Table<Item>().ToListAsync();

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
    }
}