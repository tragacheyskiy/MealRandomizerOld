using MealRandomizer.Service;
using System;

namespace MealRandomizer.Models
{
    public sealed class CategoryTranslator
    {
        private static readonly Lazy<CategoryTranslator> lazy = new Lazy<CategoryTranslator>();

        private CategoryTranslator() { }

        public static CategoryTranslator Instance => lazy.Value;

        public string GetTranslatedName(ProductCategory category)
        {
            if (DeviceCulture.IsRussian)
            {
                return category switch
                {
                    ProductCategory.BAKE => "ВЫПЕЧКА",
                    ProductCategory.BEAN => "БОБОВЫЕ",
                    ProductCategory.CHOCOLATE => "ШОКОЛАД",
                    ProductCategory.DAIRY_PRODUCTS => "МОЛОЧНЫЕ ПРОДУКТЫ",
                    ProductCategory.DRINKS => "НАПИТКИ",
                    ProductCategory.EGGS => "ЯЙЦА",
                    ProductCategory.FRUITS_AND_BERRIES => "ФРУКТЫ И ЯГОДЫ",
                    ProductCategory.GRAIN => "КРУПЫ",
                    ProductCategory.MEAT => "МЯСО",
                    ProductCategory.MEAT_PRODUCTS => "МЯСНЫЕ ПРОДУКТЫ",
                    ProductCategory.MUSHROOMS => "ГРИБЫ",
                    ProductCategory.NUTS => "ОРЕХИ",
                    ProductCategory.OIL => "МАСЛО",
                    ProductCategory.PASTA => "МАКАРОНЫ",
                    ProductCategory.SEAFOOD => "МОРЕПРОДУКТЫ",
                    ProductCategory.VEGETABLES => "ОВОЩИ",
                    _ => ""
                };
            }
            else
            {
                return category.ToString().Replace('_', ' ');
            }
        }
    }
}
