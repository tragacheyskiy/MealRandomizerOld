using System.Collections.Generic;
using System.Threading.Tasks;

namespace MealRandomizer.Models
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> DeleteItemAsync(T item);
        Task<bool> UpdateItemAsync(T item, T newItem);
        List<T> GetItems();
    }
}
