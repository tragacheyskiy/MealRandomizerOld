using System;

namespace MealRandomizer.Models
{
    [Serializable]
    public struct Nutrients : IEquatable<Nutrients>
    {
        public float Proteins { get; }
        public float Fats { get; }
        public float Carbohydrates { get; }
        public float Calories { get; }

        public Nutrients(float proteins, float fats, float carbohydrates, float calories)
        {
            Proteins = proteins;
            Fats = fats;
            Carbohydrates = carbohydrates;
            Calories = calories;
        }

        public static Nutrients operator +(Nutrients left, Nutrients right)
        {
            return new Nutrients
            (
                proteins: left.Proteins + right.Proteins,
                fats: left.Fats + right.Fats,
                carbohydrates: left.Carbohydrates + right.Carbohydrates,
                calories: left.Calories + right.Calories
            );
        }

        public static Nutrients operator -(Nutrients left, Nutrients right)
        {
            return new Nutrients
            (
                proteins: left.Proteins - right.Proteins,
                fats: left.Fats - right.Fats,
                carbohydrates: left.Carbohydrates - right.Carbohydrates,
                calories: left.Calories - right.Calories
            );
        }

        public static Nutrients operator *(Nutrients left, float right)
        {
            return new Nutrients
            (
                proteins: left.Proteins * right,
                fats: left.Fats * right,
                carbohydrates: left.Carbohydrates * right,
                calories: left.Calories * right
            );
        }

        public static Nutrients operator /(Nutrients left, float right)
        {
            return new Nutrients
            (
                proteins: left.Proteins / right,
                fats: left.Fats / right,
                carbohydrates: left.Carbohydrates / right,
                calories: left.Calories / right
            );
        }

        public bool Equals(Nutrients other)
        {
            return Proteins.Equals(other.Proteins)
                && Fats.Equals(other.Fats)
                && Carbohydrates.Equals(other.Carbohydrates)
                && Calories.Equals(other.Calories);
        }
    }
}
