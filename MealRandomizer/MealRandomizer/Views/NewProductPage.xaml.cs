using MealRandomizer.Models;
using MealRandomizer.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MealRandomizer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewProductPage : ContentPage
    {
        private readonly Color HalfOpacityRed = Color.FromHex("#50FF0000");
        private List<CategoryVM> _categories;

        public IList Categories => _categories;

        public ICommand CloseButtonCommand => new Command(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public ICommand AddButtonCommand => new Command(async () =>
        {
            if (IsInputCorrect())
            {
                await Navigation.PopModalAsync();
            }
        });

        public NewProductPage(ProductCategory category)
        {
            InitializeComponent();
            InitializeCategories();
            SetCurrentCategory(category);
            CloseButton.Command = CloseButtonCommand;
            AddButton.Command = AddButtonCommand;
        }

        private void InitializeCategories()
        {
            _categories = new List<CategoryVM>();
            for (int i = 1; i < Enum.GetNames(typeof(ProductCategory)).Length; i++)
            {
                _categories.Add(new CategoryVM((ProductCategory)i, null));
            }
            CategoryPicker.ItemsSource = Categories;
        }

        private void CreateNewProduct()
        {
            IsInputCorrect();
        }

        private bool IsInputCorrect()
        {
            return IsProductNameCorrect()
                 & IsCategoryCorrect()
                 & IsNutrientsCorrect();
        }

        private bool IsCategoryCorrect()
        {
            if (CategoryPicker.SelectedIndex == -1)
            {
                CategoryPicker.BackgroundColor = HalfOpacityRed;
                return false;
            }
            return true;
        }

        private bool IsNutrientsCorrect()
        {
            return IsNutrientCorrect(ProteinsEntry)
                 & IsNutrientCorrect(FatsEntry)
                 & IsNutrientCorrect(CarbohydratesEntry)
                 & IsNutrientCorrect(CaloriesEntry);
        }

        private bool IsNumberCorrect(Entry entry)
        {
            var sepIndex = entry.Text.LastIndexOfAny(new char[] { ',', '.' });
            return sepIndex != 0
                && sepIndex != entry.Text.Length - 1;
            
        }

        private bool IsNutrientCorrect(Entry entry)
        {
            if (string.IsNullOrWhiteSpace(entry.Text) || !IsNumberCorrect(entry))
            {
                entry.BackgroundColor = HalfOpacityRed;
                return false;
            }
            return true;
        }

        private bool IsProductNameCorrect()
        {
            string name = NameEntry.Text;
            if (string.IsNullOrWhiteSpace(name) || !name.All(c => char.IsLetter(c)))
            {
                NameEntry.BackgroundColor = HalfOpacityRed;
                return false;
            }
            return true;
        }

        private void SetCurrentCategory(ProductCategory category)
        {
            if (category != ProductCategory.ALL)
            {
                CategoryPicker.SelectedIndex = (int)category - 1;
            }
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            var entry = (Entry)sender;
            entry.BackgroundColor = Color.Transparent;
        }

        private void CategoryPicker_Focused(object sender, FocusEventArgs e)
        {
            var picker = (Picker)sender;
            picker.BackgroundColor = Color.Transparent;
        }
    }
}