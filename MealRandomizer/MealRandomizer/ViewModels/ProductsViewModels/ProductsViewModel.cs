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
        private List<ProductViewModel> productsSource;
        private ProductViewModel selectedProduct;
        private string searchText = string.Empty;

        private List<ProductViewModel> ProductsSource
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
        public ObservableCollection<ProductViewModel> Products { get; } = new ObservableCollection<ProductViewModel>();
        public bool IsClearButtonVisible
        {
            get => isClearButtonVisible;
            set => SetProperty(ref isClearButtonVisible, value);
        }
        public CategoryViewModel CurrentCategory { get; }
        public ImageSource Image { get; }
        public ProductViewModel SelectedProduct
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
            var result = from productVM in ProductsSource
                         where productVM.Product.Name.Contains(searchText.ToLowerInvariant())
                         select productVM;
            foreach (ProductViewModel productVM in result)
            {
                Products.Add(productVM);
            }
        }

        private void RefreshProducts()
        {
            Products.Clear();
            foreach (ProductViewModel productVM in ProductsSource)
            {
                Products.Add(productVM);
            }
        }

        private void InitializeCommands()
        {
            BackButtonCommand = new Command(PopPage);

            ClearButtonCommand = new Command(() => SearchText = string.Empty);

            AddButtonCommand = new Command(() =>
            {
                if (!MainPage.IsBusy)
                {
                    NewProductPage page = new NewProductPage() { BindingContext = new NewProductViewModel(CurrentCategory) };
                    PushPageModal(page);
                }
            });

            SelectProductCommand = new Command(() =>
            {
                if (!MainPage.IsBusy)
                {
                    ProductDetailPage page = new ProductDetailPage() { BindingContext = new ProductDetailViewModel(SelectedProduct) };
                    PushPageModal(page);
                    SelectedProduct = null;
                }
            });
        }
    }
}
