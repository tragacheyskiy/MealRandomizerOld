using MealRandomizer.Models;
using MealRandomizer.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        private const string IMAGE_PATH = "MealRandomizer.ProductCategoryPics.";
        private static readonly Lazy<CategoriesViewModel> instance = new Lazy<CategoriesViewModel>(() => new CategoriesViewModel());
        private readonly CategoryViewModel categoryDefault = new CategoryViewModel(ProductCategory.ALL);
        private CategoryWithImageViewModel selectedCategory;

        public static CategoriesViewModel Instance => instance.Value;
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
            AddButtonCommand = new Command(async () =>
            {
                if (!Page.IsBusy)
                {
                    Page.IsBusy = true;
                    NewProductPage page = new NewProductPage() { BindingContext = new NewProductViewModel(categoryDefault) };
                    await Page.Navigation.PushModalAsync(page);
                    Page.IsBusy = false;
                }
            });
            SearchButtonCommand = new Command(async () =>
            {
                if (!Page.IsBusy)
                {
                    Page.IsBusy = true;
                    ProductsSearchPage page = new ProductsSearchPage() { BindingContext = new ProductsSearchViewModel(categoryDefault) };
                    await Page.Navigation.PushModalAsync(page);
                    Page.IsBusy = false;
                }
            });
            SelectCategoryCommand = new Command(async () =>
            {
                if (!Page.IsBusy)
                {
                    Page.IsBusy = true;
                    ProductsPage page = new ProductsPage()
                    {
                        BindingContext = new ProductsViewModel(SelectedCategory)
                    };
                    await Page.Navigation.PushAsync(page);
                    SelectedCategory = null;
                    Page.IsBusy = false;
                }
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
                ImageSource image = ImageSource.FromResource($"{IMAGE_PATH}{categoryVM.GetCategory().ToString().ToLowerInvariant()}.jpg");
                categoriesSource.Add(new CategoryWithImageViewModel(categoryVM, image));
            }
            return categoriesSource;
        }
    }
}
