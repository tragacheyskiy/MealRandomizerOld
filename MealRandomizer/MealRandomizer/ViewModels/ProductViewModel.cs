using MealRandomizer.Models;
using System;

namespace MealRandomizer.ViewModels
{
    public class ProductViewModel : IComparable<ProductViewModel>, IEquatable<ProductViewModel>
    {
        public Product Product { get; }
        public string Name { get; }
        public string Proteins { get; }
        public string Fats { get; }
        public string Carbohydrates { get; }
        public string Calories { get; }
        public CategoryViewModel Category { get; }

        public ProductViewModel(Product product)
        {
            Product = product;
            Category = new CategoryViewModel(product.Category);
            Name = char.ToUpperInvariant(product.Name[0]) + product.Name.Substring(1);
            Proteins = product.NutrientsPerHundredGrams.Proteins.ToString();
            Fats = product.NutrientsPerHundredGrams.Fats.ToString();
            Carbohydrates = product.NutrientsPerHundredGrams.Carbohydrates.ToString();
            Calories = product.NutrientsPerHundredGrams.Calories.ToString();
        }

        public bool Equals(ProductViewModel other)
        {
            return Product.Equals(other.Product);
        }

        public int CompareTo(ProductViewModel other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
