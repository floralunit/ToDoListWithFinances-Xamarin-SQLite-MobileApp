using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FloralMobileApp.Services
{
    public interface IDataStore<T>
    {
        Task<int> AddOrUpdateItemAsync(T item);
        Task<int> DeleteItemAsync(T item);
        Task<T> GetItemAsync(int id);
        Task<List<T>> GetItemsAsync();
    }
}
