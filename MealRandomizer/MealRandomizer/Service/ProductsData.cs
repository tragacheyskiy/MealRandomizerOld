using MealRandomizer.Models;
using MealRandomizer.ViewModels.ProductsViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MealRandomizer.Service
{
    internal class ProductsData
    {
        private static Lazy<ProductsData> instance = new Lazy<ProductsData>(() => new ProductsData());

        public static ProductsData Instance => instance.Value;

        private List<ProductViewModel> Products { get; }

        public event Action<ProductsData> ProductsSourceChanged;

        public DataStore<Product> ProductsSource { get; }

        private ProductsData()
        {
            ProductsSource = new DataStore<Product>("Products");
            Products = GetProducts();
        }

        public List<ProductViewModel> GetProductsByCategory(CategoryViewModel category)
        {
            if (category.GetCategory() == ProductCategory.ALL)
            {
                return Products;
            }
            var productsByCategory = from productVM in Products
                                     where productVM.Category.Equals(category)
                                     select productVM;
            return productsByCategory.ToList();
        }

        public Task<bool> UpdateProductAsync(ProductViewModel productVM, ProductViewModel newProductVM)
        {
            Products.Remove(productVM);
            Products.Add(newProductVM);
            Products.Sort();
            OnProductsSourceChanged();
            return ProductsSource.UpdateItemAsync(productVM.Product, newProductVM.Product);
        }

        public Task<bool> AddProductAsync(ProductViewModel productVM)
        {
            Products.Add(productVM);
            Products.Sort();
            OnProductsSourceChanged();
            return ProductsSource.AddItemAsync(productVM.Product);
        }

        public Task<bool> DeleteProductAsync(ProductViewModel productVM)
        {
            Products.Remove(productVM);
            Products.Sort();
            OnProductsSourceChanged();
            return ProductsSource.DeleteItemAsync(productVM.Product);
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

        private void OnProductsSourceChanged()
        {
            ProductsSourceChanged?.Invoke(this);
        }
    }
}
