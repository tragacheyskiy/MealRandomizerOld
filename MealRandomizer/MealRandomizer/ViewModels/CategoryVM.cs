using MealRandomizer.Models;
using MealRandomizer.Service;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels
{
    public class CategoryVM
    {
        private ProductCategory _category;

        public string Category => CategoryTranslator.Instance.GetTranslatedName(_category);
        public ImageSource Image { get; set; }

        public CategoryVM(ProductCategory category, ImageSource image)
        {
            _category = category;
            Image = image;
        }

        public ProductCategory GetCategory() => _category;

        public override string ToString() => Category;
    }
}
