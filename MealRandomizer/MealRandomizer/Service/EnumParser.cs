using System;
using System.Collections.Generic;
using System.Text;
using MealRandomizer.Models;

namespace MealRandomizer.Service
{
    public static class EnumParser
    {
        public static ProductCategory GetCategoryFromString(string name)
        {
            return name switch
            {
                "ВЫПЕЧКА" => ProductCategory.BAKE,
                "БОБОВЫЕ" => ProductCategory.BEAN,
                "НАПИТКИ" => ProductCategory.BEAN,
                "ЯЙЦА" => ProductCategory.EGG,
                "РЫБА" => ProductCategory.FISH,
                "ФРУКТЫ" => ProductCategory.FRUIT,
                "КРУПЫ" => ProductCategory.GRAIN,
                "МЯСО" => ProductCategory.MEAT,
                "МОЛОЧКА" => ProductCategory.MILK,
                "ГРИБЫ" => ProductCategory.MUSHROOMS,
                "ОРЕХИ" => ProductCategory.NUTS,
                "МАСЛО" => ProductCategory.OIL,
                "МАКАРОНЫ" => ProductCategory.PASTA,
                "ОВОЩИ" => ProductCategory.VEGETABLE,
                _ => ProductCategory.ALL,
            };
        }

        public static string TranslateToRussian(string name)
        {
            return name switch
            {
                "BAKE" => "ВЫПЕЧКА",
                "BEAN" => "БОБОВЫЕ",
                "DRINK" => "НАПИТКИ",
                "EGG" => "ЯЙЦА",
                "FISH" => "РЫБА",
                "FRUIT" => "ФРУКТЫ",
                "GRAIN" => "КРУПЫ",
                "MEAT" => "МЯСО",
                "MILK" => "МОЛОЧКА",
                "MUSHROOMS" => "ГРИБЫ",
                "NUTS" => "ОРЕХИ",
                "OIL" => "МАСЛО",
                "PASTA" => "МАКАРОНЫ",
                "VEGETABLE" => "ОВОЩИ",
                _ => ""
            };
        }
    }
}
