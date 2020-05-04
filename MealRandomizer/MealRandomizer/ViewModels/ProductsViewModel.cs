using FoodRandom.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodRandom.ViewModels
{
    public class ProductsViewModel
    {
        private const int COUNT_PER_LOAD = 30;
        private int productCount = 0;
        private List<Product> productsSourse;

        public ObservableCollection<ProductViewModel> Products { get; private set; }
        public Command LoadMoreProductsCommand => new Command(async () => await Task.Run(LoadProducts));
        public string LastName => productsSourse.Last().Name;

        public ProductsViewModel()
        {
            Initialize();
        }

        private async void Initialize()
        {
            Products = new ObservableCollection<ProductViewModel>();
            await Task.Run(InitializeProductsSourse);
            await Task.Run(LoadProducts); 
        }

        private void LoadProducts()
        {
            for (int i = productCount; i < (productCount + COUNT_PER_LOAD) && i < productsSourse.Count; i++)
            {
                Products.Add(new ProductViewModel(productsSourse[i]));
            }
        }

        private void InitializeProductsSourse()
        {
            var orderedProducts = from p in GetRandomProducts(1000)
                                  orderby p.Name
                                  select p;
            productsSourse = new List<Product>(orderedProducts);
        }

        private List<Product> GetRandomProducts(int amount)
        {
            Random rndm = new Random();
            List<Product> products = new List<Product>();
            string str;
            for (int i = 0; i < amount; i++)
            {
                str = "";
                for (int j = 0; j < 5; j++)
                {
                    str += (char)rndm.Next('a', 'z' + 1);
                }
                Nutrients nutrients = new Nutrients(rndm.Next(10, 100), rndm.Next(10, 100), rndm.Next(10, 100), rndm.Next(10, 500));
                products.Add(new Product(str, (ProductCategories)rndm.Next(0, 8), nutrients));
            }
            return products;
        }
    }
}
