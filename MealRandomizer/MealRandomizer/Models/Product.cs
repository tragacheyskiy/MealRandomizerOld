using System;

namespace MealRandomizer.Models
{
    [Serializable]
    public class Product : IComparable<Product>, IEquatable<Product>
    {
        public string Name { get; }
        public ProductCategories Category { get; }
        public Nutrients NutrientsPerHundredGrams { get; }

        public Product(string name, ProductCategories category, Nutrients nutrientsPerHundredGrams)
        {
            Name = name.ToUpperInvariant();
            Category = category;
            NutrientsPerHundredGrams = nutrientsPerHundredGrams;
        }

        public bool Equals(Product other)
        {
            return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public int CompareTo(Product other)
        {
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return $"{Name} ({Category})\n    " +
                   $"Proteins per 100g: {NutrientsPerHundredGrams.Proteins} g.\n    " +
                   $"Fats per 100g: {NutrientsPerHundredGrams.Fats} g.\n    " +
                   $"Carbohydrates per 100g: {NutrientsPerHundredGrams.Fats} g.\n    " +
                   $"Calories per 100g: {NutrientsPerHundredGrams.Calories} kcal";
        }
    }
}
