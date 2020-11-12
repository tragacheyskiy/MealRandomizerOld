using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace MealRandomizer.Models
{
    [Serializable]
    public class Recipe : IEquatable<Recipe>
    {
        public string Name { get; set; }
        public string Formula { get; set; }
        public Nutrients NutrientsTotal { get; private set; }
        public Nutrients NutrientsPerHundredGrams => NutrientsTotal / TotalWeightInGrams * 100f;
        public List<ProductWithWeight> ProductsWithWeight { get; private set; }
        public int TotalWeightInGrams { get; private set; }

        public Recipe(string name, string formula, List<ProductWithWeight> products)
        {
            Name = name.ToUpperInvariant();
            Formula = formula;
            ProductsWithWeight = products;
            CountTotalWeight();
            CountTotalNutrients();
        }

        public bool Equals(Recipe other)
        {
            return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public void AddProduct(ProductWithWeight newPair)
        {
            if (!ProductsWithWeight.Exists(pair => pair.Equals(newPair)))
            {
                ProductsWithWeight.Add(newPair);
                CountTotalWeight();
                CountTotalNutrients();
            }
        }

        public void RemoveProduct(ProductWithWeight oldPair)
        {
            if (ProductsWithWeight.Exists(pair => pair.Equals(oldPair)))
            {
                ProductsWithWeight.Remove(oldPair);
                CountTotalWeight();
                CountTotalNutrients();
            }
        }

        private void CountTotalWeight()
        {
            TotalWeightInGrams = 0;
            foreach(var pair in ProductsWithWeight)
            {
                TotalWeightInGrams += pair.WeightInGrams;
            }
        }

        private void CountTotalNutrients()
        {
            NutrientsTotal = new Nutrients();
            foreach(var pair in ProductsWithWeight)
            {
                NutrientsTotal += pair.Product.NutrientsPerHundredGrams / 100f * pair.WeightInGrams;
            }
        }
    }
}
