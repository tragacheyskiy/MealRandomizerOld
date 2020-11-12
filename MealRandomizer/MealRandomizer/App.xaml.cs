using MealRandomizer.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MealRandomizer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static List<Product> GetRandomProducts(int amount)
        {
            List<Product> products = new List<Product>();
            Random random = new Random();
            string name;
            for (int i = 0; i < amount; i++)
            {
                name = "";
                for (int j = 0; j < random.Next(5, 10); j++)
                {
                    name += (char)random.Next('a', 'z' + 1);
                }
                ProductCategory category = (ProductCategory)random.Next(1, Enum.GetNames(typeof(ProductCategory)).Length);
                Nutrients nutrients = new Nutrients(
                    proteins: random.Next(10, 501),
                    fats: random.Next(10, 501),
                    carbohydrates: random.Next(10, 501),
                    calories: random.Next(100, 1001));
                products.Add(new Product(name, category, nutrients));
            }
            products.Sort();
            return products;
        }
    }
}
