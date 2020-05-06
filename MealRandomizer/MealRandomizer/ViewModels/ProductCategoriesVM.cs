using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealRandomizer.Service;
using MealRandomizer.Models;

namespace MealRandomizer.ViewModels
{
    public class ProductCategoriesVM
    {
        public IEnumerable<string> Categories { get; private set; }

        public ProductCategoriesVM()
        {
            Initialize();
        }

        private async void Initialize()
        {
            Categories = await Task.FromResult(GetCategories());
        }

        private IEnumerable<string> GetCategories()
        {
            var categories = from name in Enum.GetNames(typeof(ProductCategory)).Skip(1)
                             select EnumParser.TranslateToRussian(name)
                             into translatedName
                             orderby translatedName
                             select translatedName;
            return categories;
        }
    }
}
