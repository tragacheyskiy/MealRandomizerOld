using MealRandomizer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels
{
    public class CategoriesVM
    {
        private const string IMAGE_PATH = "MealRandomizer.ProductCategoryPics.";
        private const int TOTAL_CATEGORIES = 16;
        private readonly IList<CategoryVM> sourse;
        public ObservableCollection<CategoryVM> Categories { get; private set; }

        public CategoriesVM()
        {
            sourse = new List<CategoryVM>();
            InitializeCategories();
        }

        private void InitializeCategories()
        {
            int categoryValue = TOTAL_CATEGORIES;
            while (categoryValue > 0)
            {
                ProductCategory category = (ProductCategory)categoryValue--;
                string imagePath = $"{IMAGE_PATH}{category.ToString().ToLowerInvariant()}.jpg";
                sourse.Add(new CategoryVM(category, ImageSource.FromResource(imagePath)));
            }
            Categories = new ObservableCollection<CategoryVM>(sourse.OrderBy(category => category.Category));
        }
    }
}
