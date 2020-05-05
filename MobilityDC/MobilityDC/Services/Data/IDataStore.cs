using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobilityDC.Data.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemsAsync(object item, string userId);
        Task<bool> DeleteItemAsync(string userId);
        Task<object> GetItemAsync(string userId);
        Task<object> GetItemsAsync(object item, string userId);
    }
}
