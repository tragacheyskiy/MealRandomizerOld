﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealRandomizer.Models
{
    class RandomProductsCollection
    {
        private List<Product> Products { get; set; } = null;

        public RandomProductsCollection(int amount)
        {
            Products = GetRandomProducts(amount);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await Task.FromResult(Products);
        }

        public async Task<bool> AddProductAsync(Product newProduct)
        {
            return await Task.FromResult(AddProduct(newProduct));
        }

        public async Task<bool> RemoveProductAsync(Product oldProduct)
        {
            return await Task.FromResult(RemoveProduct(oldProduct));
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            return await Task.FromResult(RemoveProduct(product) && AddProduct(product));
        }

        private bool AddProduct(Product product)
        {
            if (!IsThereProduct(product))
            {
                Products.Add(product);
                return true;
            }
            return false;
        }

        private bool RemoveProduct(Product product)
        {
            if(IsThereProduct(product))
            {
                Products.Remove(product);
                return true;
            }
            return false;
        }

        private bool IsThereProduct(Product testProduct) => Products.Any(product => product.Equals(testProduct));

        private List<Product> GetRandomProducts(int amount)
        {
            Random rndm = new Random();
            List<Product> products = new List<Product>();
            string str;
            for (int i = 0; i < amount; i++)
            {
                str = "";
                for (int j = 0; j < 7; j++)
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
