using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRandom.Model
{
    public class ItemsCollection<T> where T : IEquatable<T>
    {
        private readonly string PATH;
        private List<T> Items { get; set; } = null;

        public ItemsCollection(string path)
        {
            PATH = path;
        }

        public List<T> GetItems()
        {
            if (Items == null)
            {
                Items = DataProvider.Deserialize<T>(PATH);
            }
            return Items;
        }

        public bool AddItem(T newItem)
        {
            if (!Items.Any(item => item.Equals(newItem)))
            {
                SaveItem(newItem);
                return true;
            }
            return false;
        }

        private void SaveItem(T item)
        {
            if (Items == null)
            {
                Items = new List<T>();
            }
            Items.Add(item);
            DataProvider.Serialize(Items.ToList(), PATH);
        }
    }
}
