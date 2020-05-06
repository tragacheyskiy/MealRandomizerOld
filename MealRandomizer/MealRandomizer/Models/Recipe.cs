using System;
using System.Collections.Generic;

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
        public uint TotalWeightInGrams { get; private set; }

        public Recipe(string name, string formula, List<ProductWithWeight> products)
        {
            Name = name.ToUpperInvariant();
            Formula = formula;
            ProductsWithWeight = products;
            UpdateTotalWeight();
            UpdateNutrients();
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
                UpdateTotalWeight();
                UpdateNutrients();
            }
        }

        public void RemoveProduct(ProductWithWeight oldPair)
        {
            if (ProductsWithWeight.Exists(pair => pair.Equals(oldPair)))
            {
                ProductsWithWeight.Remove(oldPair);
                UpdateTotalWeight();
                UpdateNutrients();
            }
        }

        private void UpdateTotalWeight()
        {
            TotalWeightInGrams = 0;
            ProductsWithWeight.ForEach(pair =>
            {
                TotalWeightInGrams += pair.WeightInGrams;
            });
        }

        private void UpdateNutrients()
        {
            NutrientsTotal = new Nutrients();
            ProductsWithWeight.ForEach(pair =>
            {
                NutrientsTotal += pair.Product.NutrientsPerHundredGrams / 100f * pair.WeightInGrams;
            });
        }
    }
}
