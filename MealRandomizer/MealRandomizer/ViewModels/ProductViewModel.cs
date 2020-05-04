using FoodRandom.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FoodRandom.ViewModels
{
    public class ProductViewModel
    {
        private readonly Product product;

        public string Name => product.Name;
        public string Category => product.Category.ToString();
        public string Nutrients
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

        public ProductViewModel(Product product)
        {
            this.product = product;
        }
    }
}
