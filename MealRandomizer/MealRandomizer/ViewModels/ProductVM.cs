using MealRandomizer.Models;

namespace MealRandomizer.ViewModels
{
    public class ProductVM
    {
        private readonly Product product;

        public string Name => product.Name;
        public string Category => product.Category.ToString();
        public string Proteins => $"{product.NutrientsPerHundredGrams.Proteins} гр.";
        public string Fats => $"{product.NutrientsPerHundredGrams.Fats} гр.";
        public string Carbohydrates => $"{product.NutrientsPerHundredGrams.Carbohydrates} гр.";
        public string Calories => $"{product.NutrientsPerHundredGrams.Calories} кКал.";
        private string Nutrients
        {
            get
            {
                return $"На 100 грамм продукта:\n" +
                       $"Б: {product.NutrientsPerHundredGrams.Proteins} гр. " +
                       $"Ж: {product.NutrientsPerHundredGrams.Fats} гр. " +
                       $"У: {product.NutrientsPerHundredGrams.Carbohydrates} гр.\n" +
                       $"Калории: {product.NutrientsPerHundredGrams.Calories} кКал.";
            }
        }

        public ProductVM(Product product)
        {
            this.product = product;
        }
    }
}
