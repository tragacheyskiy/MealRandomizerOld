using MealRandomizer.Models;
using MealRandomizer.Service;
using MealRandomizer.Views.ProductsViews;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels.ProductsViewModels
{
    public sealed class ProductsSearchViewModel : BaseViewModel
    {
        private bool isClearButtonVisible;
        private string searchText = string.Empty;

        private List<Product> ProductsSource { get; }

        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
        public Product SelectedProduct { get; set; }
        public bool IsClearButtonVisible
        {
            get => isClearButtonVisible;
            set => SetProperty(ref isClearButtonVisible, value);
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
        public Command BackButtonCommand { get; private set; }
        public Command SelectProductCommand { get; private set; }

        public ProductsSearchViewModel(CategoryViewModel currentCategory)
        {
            ProductsSource = ProductsData.Instance.GetProductsByCategory(currentCategory);
            InitializeCommands();
        }

        private void Search(string searchText)
        {
            Products.Clear();

            if (string.IsNullOrEmpty(searchText))
                return;

            var result = from product in ProductsSource
                         where product.Name.Contains(searchText.ToLowerInvariant())
                         select product;

            foreach (Product product in result)
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

                PopPageModal();
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
