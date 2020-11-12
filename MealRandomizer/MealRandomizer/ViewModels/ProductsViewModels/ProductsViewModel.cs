using MealRandomizer.Models;
using MealRandomizer.Service;
using MealRandomizer.Views.ProductsViews;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels.ProductsViewModels
{
    public sealed class ProductsViewModel : BaseViewModel
    {
        private bool isClearButtonVisible;
        private List<Product> productsSource;
        private Product selectedProduct;
        private string searchText = string.Empty;

        private List<Product> ProductsSource
        {
            get => productsSource;
            set
            {
                productsSource = value;
                RefreshProducts();
            }
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                IsClearButtonVisible = value != string.Empty;
                SetProperty(ref searchText, value);
                Search(searchText);
            }
        }
        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
        public bool IsClearButtonVisible
        {
            get => isClearButtonVisible;
            set => SetProperty(ref isClearButtonVisible, value);
        }
        public CategoryViewModel CurrentCategory { get; }
        public ImageSource Image { get; }
        public Product SelectedProduct
        {
            get => selectedProduct;
            set => SetProperty(ref selectedProduct, value);
        }
        public Command AddButtonCommand { get; private set; }
        public Command BackButtonCommand { get; private set; }
        public Command ClearButtonCommand { get; private set; }
        public Command SelectProductCommand { get; private set; }

        public ProductsViewModel(CategoryWithImageViewModel categoryWithImageVM)
        {
            CurrentCategory = categoryWithImageVM.CategoryVM;
            Image = categoryWithImageVM.Image;
            ProductsSource = ProductsData.Instance.GetProductsByCategory(CurrentCategory);
            ProductsData.Instance.ProductsSourceChanged += productsData => ProductsSource = productsData.GetProductsByCategory(CurrentCategory);
            InitializeCommands();
        }

        private void Search(string searchText)
        {
            Products.Clear();

            if (string.IsNullOrEmpty(searchText))
            {
                RefreshProducts();
                return;
            }

            var result = from product in ProductsSource
                         where product.Name.Contains(searchText.ToLowerInvariant())
                         select product;

            foreach (Product product in result)
            {
                Products.Add(product);
            }
        }

        private void RefreshProducts()
        {
            Products.Clear();

            foreach (Product product in ProductsSource)
            {
                Products.Add(product);
            }
        }

        private void InitializeCommands()
        {
            BackButtonCommand = new Command(() =>
            {
                if (MainPage.IsBusy)
                    return;

                PopPage();
            });

            ClearButtonCommand = new Command(() => SearchText = string.Empty);

            AddButtonCommand = new Command(() =>
            {
                if (MainPage.IsBusy)
                    return;

                NewProductPage page = new NewProductPage() { BindingContext = new NewProductViewModel(CurrentCategory) };
                PushPageModal(page);
            });

            SelectProductCommand = new Command(() =>
            {
                if (MainPage.IsBusy || SelectedProduct == null)
                    return;

                ProductDetailPage page = new ProductDetailPage() { BindingContext = new ProductDetailViewModel(SelectedProduct) };
                PushPageModal(page);
                SelectedProduct = null;
            });
        }
    }
}
