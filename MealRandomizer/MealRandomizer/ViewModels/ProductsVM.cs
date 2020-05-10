using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MealRandomizer.Models;

namespace MealRandomizer.ViewModels
{
    public class ProductsVM
    {
        private static List<Product> ProductsSourse { get; } = new RandomProductsCollection(10000).GetProductsAsync().Result;
        private const int COUNT_PER_LOAD = 30;
        private int productsCurrentIndex = 0;
        private int productsMaxCount = COUNT_PER_LOAD;

        public ObservableCollection<ProductVM> Products { get; private set; }
        public Command LoadMoreProductsCommand => new Command(async () => await Task.Run(LoadProducts));

        public ProductsVM()
        {
            Initialize();
        }

        private async void Initialize()
        {
            Products = new ObservableCollection<ProductVM>();
            await Task.Run(LoadProducts); 
        }

        private void LoadProducts()
        {
            while(productsCurrentIndex < productsMaxCount && productsCurrentIndex < ProductsSourse.Count)
            {
                Products.Add(new ProductVM(ProductsSourse[productsCurrentIndex++]));
            }
            productsMaxCount += COUNT_PER_LOAD;
        }
    }
}
