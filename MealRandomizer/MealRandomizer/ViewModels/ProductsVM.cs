using MealRandomizer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels
{
    public class ProductsVM
    {
        private List<Product> ProductsSourse { get; set; }
        private const int COUNT_PER_LOAD = 40;
        private int productsCurrentIndex = 0;
        private int productsMaxCount = COUNT_PER_LOAD;

        public ImageSource Image { get; } = ImageSource.FromResource("MealRandomizer.ProductCategoryPics.nuts.jpg");
        public string Category { get; private set; }
        public ObservableCollection<ProductVM> Products { get; private set; }
        public Command LoadMoreProductsCommand => new Command(async () => await Task.Run(LoadProducts));

        public ProductsVM(ProductCategory category)
        {
            Products = new ObservableCollection<ProductVM>();
            Initialize(category);
        }

        private async void Initialize(ProductCategory category)
        {
            ProductsSourse = await App.RandomProductsCollection.GetProductsByCategoryAsync(category);
            Category = $"{CategoryTranslator.Instance.GetTranslatedName(category)} ({ProductsSourse.Count})";
            await Task.Run(LoadProducts);
        }

        private void LoadProducts()
        {
            while (productsCurrentIndex < productsMaxCount && productsCurrentIndex < ProductsSourse.Count)
            {
                Products.Add(new ProductVM(ProductsSourse[productsCurrentIndex++]));
            }
            productsMaxCount += COUNT_PER_LOAD;
        }
    }
}
