﻿using MealRandomizer.Models;
using System;

namespace MealRandomizer.Service
{
    public sealed class CategoryTranslator
    {
        private static readonly Lazy<CategoryTranslator> instance = new Lazy<CategoryTranslator>(() => new CategoryTranslator());

        public static CategoryTranslator Instance => instance.Value;

        private CategoryTranslator() { }

        public string GetTranslatedName(ProductCategory category)
        {
            if (DeviceCulture.IsRussian)
            {
                return GetTranslate(category);
            }
            else
            {
                return category.ToString().Replace('_', ' ');
            }
        }

        private string GetTranslate(ProductCategory category)
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
                _ => "ALL"
            };
        }
    }
}
