namespace MealRandomizer.Models
{
    public class ProductWithWeight
    {
        public Product Product { get; }
        public uint WeightInGrams { get; }

        public ProductWithWeight(Product product, uint weightInGrams)
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
