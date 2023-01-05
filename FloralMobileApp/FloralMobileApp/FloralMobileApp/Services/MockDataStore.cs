using FloralMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloralMobileApp.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Title = "First item", IsCompleted = false },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Second item", IsCompleted = false },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Third item", IsCompleted = false },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Fourth item", IsCompleted = false },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Fifth item", IsCompleted = false },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Sixth item", IsCompleted = false }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            items.Remove(item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}