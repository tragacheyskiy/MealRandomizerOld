using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MealRandomizer.Models
{
    public class DataStore<T> : IDataStore<T> where T : IComparable<T>, IEquatable<T>
    {
        private readonly ISerializationProvider serializationProvider;
        private readonly string fileName;
        private readonly List<T> items;

        public DataStore(string fileName = null)
        {
            serializationProvider = DependencyService.Get<ISerializationProvider>();
            this.fileName = fileName;
            try
            {
                items = serializationProvider.Deserialize<T>(fileName);
            }
            catch
            {
                items = new List<T>();
            }
        }

        public List<T> GetItems() => items;

        public Task<bool> AddItemAsync(T item)
        {
            bool isThereNoItem = !items.Contains(item);
            if (isThereNoItem)
            {
                items.Add(item);
                items.Sort();
                serializationProvider.Serialize(items, fileName);
            }
            return Task.FromResult(isThereNoItem);
        }

        public Task<bool> DeleteItemAsync(T item)
        {
            bool isRemoved = items.Remove(item);
            if (isRemoved)
            {
                items.Sort();
                serializationProvider.Serialize(items, fileName);
            }
            return Task.FromResult(isRemoved);
        }

        public Task<bool> UpdateItemAsync(T item, T newItem)
        {
            bool isRemoved = items.Remove(item);
            if (isRemoved)
            {
                items.Add(newItem);
                items.Sort();
                serializationProvider.Serialize(items, fileName);
            }
            return Task.FromResult(isRemoved);
        }
    }
}
