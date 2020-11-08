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

        private List<ProductViewModel> ProductsSource { get; }

        public ObservableCollection<ProductViewModel> Products { get; } = new ObservableCollection<ProductViewModel>();
        public ProductViewModel SelectedProduct { get; set; }
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
            {
                return;
            }
            var result = from productVM in ProductsSource
                         where productVM.Product.Name.Contains(searchText.ToLowerInvariant())
                         select productVM;
            foreach (ProductViewModel productVM in result)
            {
                Products.Add(productVM);
            }
        }

        private void InitializeCommands()
        {
            BackButtonCommand = new Command(PopPageModal);

            SelectProductCommand = new Command(() =>
            {
                ProductDetailPage page = new ProductDetailPage() { BindingContext = new ProductDetailViewModel(SelectedProduct) };
                PushPageModal(page);
                SelectedProduct = null;
            });
        }
    }
}
