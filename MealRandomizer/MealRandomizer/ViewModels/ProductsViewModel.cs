using MealRandomizer.Service;
using MealRandomizer.Views;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private readonly ProductsCollection productsSource;
        private ProductViewModel selectedProduct;
        private string searchText = string.Empty;

        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
                productsSource.Search(searchText, Products);
            }
        }
        public ObservableCollection<ProductViewModel> Products { get; }
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
            productsSource = ProductsCollection.Instance.SetCurrentCategory(CurrentCategory);
            Products = new ObservableCollection<ProductViewModel>(productsSource.GetProductsByCurrentCategory());
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            BackButtonCommand = new Command(async () => await Page.Navigation.PopAsync());
            ClearButtonCommand = new Command(() => SearchText = string.Empty);
            AddButtonCommand = new Command(async () =>
            {
                if (!Page.IsBusy)
                {
                    Page.IsBusy = true;
                    NewProductPage page = new NewProductPage() { BindingContext = new NewProductViewModel(productsSource) };
                    await Page.Navigation.PushModalAsync(page);
                    Page.IsBusy = false;
                }
            });
            SelectProductCommand = new Command(async () =>
            {
                if (!Page.IsBusy)
                {
                    Page.IsBusy = true;
                    ProductDetailPage page = new ProductDetailPage() { BindingContext = new ProductDetailViewModel(productsSource, SelectedProduct) };
                    await Page.Navigation.PushModalAsync(page);
                    SelectedProduct = null;
                    Page.IsBusy = false;
                }
            });
        }
    }
}
