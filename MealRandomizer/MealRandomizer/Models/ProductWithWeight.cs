namespace MealRandomizer.Models
{
    public class ProductWithWeight
    {
        public Product Product { get; }
        public int WeightInGrams { get; }

        public ProductWithWeight(Product product, int weightInGrams)
        {
            Product = product;
            WeightInGrams = weightInGrams;
        }

        public bool Equals(ProductWithWeight other)
        {
            return Product.Equals(other.Product);
        }
    }
}
