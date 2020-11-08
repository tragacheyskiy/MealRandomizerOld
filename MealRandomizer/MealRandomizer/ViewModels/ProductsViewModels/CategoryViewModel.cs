using MealRandomizer.Models;
using MealRandomizer.Service;
using System;

namespace MealRandomizer.ViewModels.ProductsViewModels
{
    public class CategoryViewModel : IComparable<CategoryViewModel>, IEquatable<CategoryViewModel>
    {
        private readonly ProductCategory category;

        public string Category { get; }

        public CategoryViewModel(ProductCategory category)
        {
            this.category = category;
            Category = ProductCategoryTranslator.Instance.GetTranslatedName(category);
        }

        public ProductCategory GetCategory() => category;

        public bool Equals(CategoryViewModel other)
        {
            return category == other.GetCategory();
        }

        public int CompareTo(CategoryViewModel other)
        {
            return Category.CompareTo(other.Category);
        }

        public override string ToString() => Category;
    }
}
