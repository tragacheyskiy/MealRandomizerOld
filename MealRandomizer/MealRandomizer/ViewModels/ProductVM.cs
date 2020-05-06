using MealRandomizer.Models;

namespace MealRandomizer.ViewModels
{
    public class ProductVM
    {
        private readonly Product _product;

        public string Name => _product.Name;
        public string Category { get; }
        public string Proteins { get; }
        public string Fats { get; }
        public string Carbohydrates { get; }
        public string Calories { get; }

        public ProductVM(Product product)
        {
            _product = product;
            Category = CategoryTranslator.Instance.GetTranslatedName(product.Category);
            Proteins = $"{_product.NutrientsPerHundredGrams.Proteins} гр.";
            Fats = $"{_product.NutrientsPerHundredGrams.Fats} гр.";
            Carbohydrates = $"{_product.NutrientsPerHundredGrams.Carbohydrates} гр.";
            Calories = $"{_product.NutrientsPerHundredGrams.Calories} кКал.";
        }
    }
}
