using MealRandomizer.Models;
using MealRandomizer.Views.ProductsViews;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels.ProductsViewModels
{
    internal sealed class CategoriesViewModel : BaseViewModel
    {
        private const string ImagePath = "MealRandomizer.ProductCategoryPics.";

        private static readonly Lazy<CategoriesViewModel> instance = new Lazy<CategoriesViewModel>(() => new CategoriesViewModel());

        public static CategoriesViewModel Instance => instance.Value;

        private readonly CategoryViewModel categoryDefault = new CategoryViewModel(ProductCategory.ALL);
        private CategoryWithImageViewModel selectedCategory;

        public List<CategoryViewModel> CategoriesSource { get; }
        public IEnumerable<CategoryWithImageViewModel> CategoriesWithImages { get; }
        public CategoryWithImageViewModel SelectedCategory
        {
            get => selectedCategory;
            set => SetProperty(ref selectedCategory, value);
        }
        public Command AddButtonCommand { get; private set; }
        public Command SearchButtonCommand { get; private set; }
        public Command SelectCategoryCommand { get; private set; }

        private CategoriesViewModel()
        {
            CategoriesSource = GetCategoriesSource();
            CategoriesWithImages = GetCategoriesWithImages();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            AddButtonCommand = new Command(() =>
            {
                NewProductPage page = new NewProductPage() { BindingContext = new NewProductViewModel(categoryDefault) };
                PushPageModal(page);
            });

            SearchButtonCommand = new Command(() =>
            {
                ProductsSearchPage page = new ProductsSearchPage() { BindingContext = new ProductsSearchViewModel(categoryDefault) };
                PushPageModal(page);
            });

            SelectCategoryCommand = new Command(() =>
            {
                ProductsPage page = new ProductsPage() { BindingContext = new ProductsViewModel(SelectedCategory) };
                PushPage(page);
                SelectedCategory = null;
            });
        }

        private List<CategoryViewModel> GetCategoriesSource()
        {
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            int totalCategories = Enum.GetNames(typeof(ProductCategory)).Length - 1;
            while (totalCategories > 0)
            {
                ProductCategory category = (ProductCategory)totalCategories--;
                categories.Add(new CategoryViewModel(category));
            }
            categories.Sort();
            return categories;
        }

        private List<CategoryWithImageViewModel> GetCategoriesWithImages()
        {
            List<CategoryWithImageViewModel> categoriesSource = new List<CategoryWithImageViewModel>();
            foreach (CategoryViewModel categoryVM in CategoriesSource)
            {
                ImageSource image = ImageSource.FromResource($"{ImagePath}{categoryVM.GetCategory().ToString().ToLowerInvariant()}.jpg");
                categoriesSource.Add(new CategoryWithImageViewModel(categoryVM, image));
            }
            return categoriesSource;
        }
    }
}
