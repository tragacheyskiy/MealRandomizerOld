using MealRandomizer.Models;
using MealRandomizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MealRandomizer.Service
{
    public class ProductsCollection
    {
        private static Lazy<ProductsCollection> instance = new Lazy<ProductsCollection>(() => new ProductsCollection());
        private List<ProductViewModel> Products { get; }

        public static ProductsCollection Instance => instance.Value;
        public DataStore<Product> ProductsSource { get; }
        public CategoryViewModel CurrentCategory { get; private set; }

        private ProductsCollection()
        {
            ProductsSource = new DataStore<Product>("Products");
            Products = GetProducts();
            CurrentCategory = new CategoryViewModel(ProductCategory.ALL);
        }

        public ProductsCollection SetCurrentCategory(CategoryViewModel currentCategory)
        {
            CurrentCategory = currentCategory;
            return this;
        }

        public Task<bool> UpdateProductAsync(ProductViewModel productVM, ProductViewModel newProductVM)
        {
            Products.Remove(productVM);
            Products.Add(newProductVM);
            Products.Sort();
            return ProductsSource.UpdateItemAsync(productVM.Product, newProductVM.Product);
        }

        public Task<bool> AddProductAsync(ProductViewModel productVM)
        {
            Products.Add(productVM);
            Products.Sort();
            return ProductsSource.AddItemAsync(productVM.Product);
        }

        public Task<bool> DeleteProductAsync(ProductViewModel productVM)
        {
            Products.Remove(productVM);
            Products.Sort();
            return ProductsSource.DeleteItemAsync(productVM.Product);
        }

        public IEnumerable<ProductViewModel> GetProductsByCurrentCategory()
        {
            if (CurrentCategory.GetCategory() == ProductCategory.ALL)
            {
                return Products;
            }
            else
            {
                return from productVM in Products
                       where productVM.Category.Equals(CurrentCategory)
                       select productVM;
            }
        }

        public void Search(string searchText, ObservableCollection<ProductViewModel> source)
        {
            source.Clear();
            if (string.IsNullOrEmpty(searchText) && CurrentCategory.GetCategory() == ProductCategory.ALL)
            {
                return;
            }
            if (string.IsNullOrEmpty(searchText))
            {
                RefreshSource(source);
                return;
            }
            var result = from productVM in GetProductsByCurrentCategory()
                         where productVM.Product.Name.Contains(searchText.ToLowerInvariant())
                         select productVM;
            foreach (ProductViewModel productVM in result)
            {
                Products.Add(productVM);
            }
        }

        private void RefreshSource(ObservableCollection<ProductViewModel> source)
        {
            source.Clear();
            foreach (ProductViewModel productVM in GetProductsByCurrentCategory())
            {
                source.Add(productVM);
            }
        }

        private List<ProductViewModel> GetProducts()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            foreach (Product product in ProductsSource.GetItems())
            {
                products.Add(new ProductViewModel(product));
            }
            return products;
        }
    }
}
