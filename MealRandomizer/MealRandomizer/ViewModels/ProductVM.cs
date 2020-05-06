using MealRandomizer.Models;
using System;
using System.Threading.Tasks;

namespace MealRandomizer.ViewModels
{
    public class ProductVM
    {
        private readonly Product product;

        public string Name => product.Name;
        public string Category { get; }
        public string Proteins => $"{product.NutrientsPerHundredGrams.Proteins} гр.";
        public string Fats => $"{product.NutrientsPerHundredGrams.Fats} гр.";
        public string Carbohydrates => $"{product.NutrientsPerHundredGrams.Carbohydrates} гр.";
        public string Calories => $"{product.NutrientsPerHundredGrams.Calories} кКал.";

        public ProductVM(Product product)
        {
            this.product = product;
            Category = Service.EnumParser.TranslateToRussian(this.product.Category.ToString());
        }
    }
}
