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

        private List<Product> Products { get; }

        public event Action<ProductsData> ProductsSourceChanged;

        public DataStore<Product> ProductsSource { get; }

        private ProductsData()
        {
            ProductsSource = new DataStore<Product>("Products");
            Products = ProductsSource.GetItems();
        }

        public List<Product> GetProductsByCategory(CategoryViewModel categoryVM)
        {
            ProductCategory category = categoryVM.GetCategory();

            if (category.Equals(ProductCategory.ALL))
            {
                return Products;
            }

            var productsByCategory = from product in Products
                                     where product.Category.Equals(category)
                                     select product;
            return productsByCategory.ToList();
        }

        public Task<bool> UpdateProductAsync(Product product, Product newProduct)
        {
            Products.Remove(product);
            Products.Add(newProduct);
            Products.Sort();
            OnProductsSourceChanged();
            return ProductsSource.UpdateItemAsync(product, newProduct);
        }

        public Task<bool> AddProductAsync(Product product)
        {
            Products.Add(product);
            Products.Sort();
            OnProductsSourceChanged();
            return ProductsSource.AddItemAsync(product);
        }

        public Task<bool> DeleteProductAsync(Product product)
        {
            Products.Remove(product);
            Products.Sort();
            OnProductsSourceChanged();
            return ProductsSource.DeleteItemAsync(product);
        }

        private void OnProductsSourceChanged()
        {
            ProductsSourceChanged?.Invoke(this);
        }
    }
}
