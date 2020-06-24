using System;

namespace MealRandomizer.Models
{
    [Serializable]
    public class Product : IComparable<Product>, IEquatable<Product>
    {
        public string Name { get; }
        public ProductCategory Category { get; }
        public Nutrients NutrientsPerHundredGrams { get; }

        public Product(string name, ProductCategory category, Nutrients nutrientsPerHundredGrams)
        {
            Name = name.ToLowerInvariant();
            Category = category;
            NutrientsPerHundredGrams = nutrientsPerHundredGrams;
        }

        public bool Equals(Product other)
        {
            return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase)
                && Category.Equals(other.Category)
                && NutrientsPerHundredGrams.Equals(other.NutrientsPerHundredGrams);
        }

        public int CompareTo(Product other)
        {
            return Name.CompareTo(other.Name);
        }

        public override string ToString() => Name;
    }
}
