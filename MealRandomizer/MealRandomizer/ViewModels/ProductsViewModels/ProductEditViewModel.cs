using MealRandomizer.Models;
using MealRandomizer.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels.ProductsViewModels
{
    public class ProductEditViewModel : BaseViewModel
    {
        private readonly Color HalfOpacityRed = Color.FromHex("#50FF0000");
        private readonly ProductViewModel productVM;
        private string name;
        private string proteins;
        private string fats;
        private string carbohydrates;
        private string calories;
        private Color nameBackgroundColor;
        private Color proteinsBackgroundColor;
        private Color fatsBackgroundColor;
        private Color carbohydratesBackgroundColor;
        private Color caloriesBackgroundColor;
        private Color categoriesBackgroundColor;

        public ProductViewModel NewProductVM { get; private set; }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public bool IsNameFocused
        {
            get => false;
            set => NameBackgroundColor = Color.Transparent;
        }
        public Color NameBackgroundColor
        {
            get => nameBackgroundColor;
            set => SetProperty(ref nameBackgroundColor, value);
        }
        public string Proteins { get => proteins; set => SetProperty(ref proteins, value); }
        public bool IsProteinsFocused
        {
            get => false;
            set => ProteinsBackgroundColor = Color.Transparent;
        }
        public Color ProteinsBackgroundColor
        {
            get => proteinsBackgroundColor;
            set => SetProperty(ref proteinsBackgroundColor, value);
        }
        public string Fats { get => fats; set => SetProperty(ref fats, value); }
        public bool IsFatsFocused
        {
            get => false;
            set => FatsBackgroundColor = Color.Transparent;
        }
        public Color FatsBackgroundColor
        {
            get => fatsBackgroundColor;
            set => SetProperty(ref fatsBackgroundColor, value);
        }
        public string Carbohydrates { get => carbohydrates; set => SetProperty(ref carbohydrates, value); }
        public bool IsCarbohydratesFocused
        {
            get => false;
            set => CarbohydratesBackgroundColor = Color.Transparent;
        }
        public Color CarbohydratesBackgroundColor
        {
            get => carbohydratesBackgroundColor;
            set => SetProperty(ref carbohydratesBackgroundColor, value);
        }
        public string Calories { get => calories; set => SetProperty(ref calories, value); }
        public bool IsCaloriesFocused
        {
            get => false;
            set => CaloriesBackgroundColor = Color.Transparent;
        }
        public Color CaloriesBackgroundColor
        {
            get => caloriesBackgroundColor;
            set => SetProperty(ref caloriesBackgroundColor, value);
        }
        public List<CategoryViewModel> CategoriesSource { get; }
        public bool IsCategoriesFocused
        {
            get => false;
            set => CategoriesBackgroundColor = Color.Transparent;
        }
        public Color CategoriesBackgroundColor
        {
            get => categoriesBackgroundColor;
            set => SetProperty(ref categoriesBackgroundColor, value);
        }
        public CategoryViewModel SelectedCategory { get; set; }
        public bool IsInputCorrect => CheckInput();

        private ProductEditViewModel(CategoryViewModel selectedCategory, ProductViewModel productVM)
        {
            this.productVM = productVM;
            name = proteins = fats = carbohydrates = calories = "";
            InitializeFields(productVM);
            CategoriesSource = CategoriesViewModel.Instance.CategoriesSource;
            SelectedCategory = selectedCategory;
        }

        public static ProductEditViewModel GetCategoryOnly(CategoryViewModel selectedCategory)
        {
            return new ProductEditViewModel(selectedCategory, null);
        }

        public static ProductEditViewModel GetCategoryWithProductVM(CategoryViewModel selectedCategory, ProductViewModel productVM)
        {
            return new ProductEditViewModel(selectedCategory, productVM);
        }

        private ProductViewModel GetProductViewModel()
        {
            return new ProductViewModel(new Product(Name.Trim(), SelectedCategory.GetCategory(), GetNutrients()));
        }

        public void Refresh()
        {
            InitializeFields(NewProductVM);
            IsNameFocused = IsProteinsFocused = IsFatsFocused = IsCarbohydratesFocused = IsCaloriesFocused = IsCategoriesFocused = false;
        }

        private Nutrients GetNutrients()
        {
            float proteins = float.Parse(Proteins);
            float fats = float.Parse(Fats);
            float carbohydrates = float.Parse(Carbohydrates);
            float calories = float.Parse(Calories);
            return new Nutrients(proteins, fats, carbohydrates, calories);
        }

        private void InitializeFields(ProductViewModel productVM)
        {
            if (productVM != null)
            {
                Name = productVM.Name;
                Proteins = productVM.Proteins;
                Fats = productVM.Fats;
                Carbohydrates = productVM.Carbohydrates;
                Calories = productVM.Calories;
            }
        }

        private bool CheckInput()
        {
            bool isCorrect = true;
            if (string.IsNullOrWhiteSpace(Name)
                || productVM == null && ProductsData.Instance.ProductsSource.GetItems().Any(product => string.Equals(product.Name, Name.ToLowerInvariant().Trim(), StringComparison.OrdinalIgnoreCase))
                || !Name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || char.IsPunctuation(c)))
            {
                NameBackgroundColor = HalfOpacityRed;
                isCorrect = false;
            }
            if (string.IsNullOrWhiteSpace(Proteins) || !IsNumberCorrect(Proteins))
            {
                ProteinsBackgroundColor = HalfOpacityRed;
                isCorrect = false;
            }
            if (string.IsNullOrWhiteSpace(Fats) || !IsNumberCorrect(Fats))
            {
                FatsBackgroundColor = HalfOpacityRed;
                isCorrect = false;
            }
            if (string.IsNullOrWhiteSpace(Carbohydrates) || !IsNumberCorrect(Carbohydrates))
            {
                CarbohydratesBackgroundColor = HalfOpacityRed;
                isCorrect = false;
            }
            if (string.IsNullOrWhiteSpace(Calories) || !IsNumberCorrect(Calories))
            {
                CaloriesBackgroundColor = HalfOpacityRed;
                isCorrect = false;
            }
            if (SelectedCategory.GetCategory() == ProductCategory.ALL)
            {
                CategoriesBackgroundColor = HalfOpacityRed;
                isCorrect = false;
            }
            if (!isCorrect)
            {
                return isCorrect;
            }
            NewProductVM = GetProductViewModel();
            if (productVM != null && productVM.Equals(NewProductVM))
            {
                NameBackgroundColor = ProteinsBackgroundColor = FatsBackgroundColor = CarbohydratesBackgroundColor = CaloriesBackgroundColor = CategoriesBackgroundColor = HalfOpacityRed;
                isCorrect = false;
            }
            return isCorrect;
        }

        private bool IsNumberCorrect(string nutrientValue)
        {
            var sepIndex = nutrientValue.LastIndexOfAny(new char[] { ',', '.' });
            return sepIndex != 0
                && sepIndex != nutrientValue.Length - 1;
        }
    }
}
