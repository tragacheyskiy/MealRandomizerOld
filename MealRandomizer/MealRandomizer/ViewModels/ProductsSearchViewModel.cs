using MealRandomizer.Models;
using MealRandomizer.Service;
using MealRandomizer.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels
{
    public class ProductsSearchViewModel : BaseViewModel
    {
        private readonly ProductsCollection productsSource;
        private bool isClearButtonVisible;
        private string searchText = string.Empty;

        public ObservableCollection<ProductViewModel> Products { get; }
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
                SetProperty(ref searchText, value);
                productsSource.Search(searchText, Products);
            }
        }
        public Command BackButtonCommand { get; private set; }
        public Command ClearButtonCommand { get; private set; }
        public Command SelectProductCommand { get; private set; }

        public ProductsSearchViewModel(ProductsCollection productsSource)
        {
            this.productsSource = productsSource;
            Products = new ObservableCollection<ProductViewModel>(productsSource.GetProductsByCurrentCategory());
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            ClearButtonCommand = new Command(() => SearchText = string.Empty);
            BackButtonCommand = new Command(async () =>
            {
                if (!Page.IsBusy)
                {
                    Page.IsBusy = true;
                    await Page.Navigation.PopModalAsync();
                    Page.IsBusy = false;
                }
            });
            SelectProductCommand = new Command(async () =>
            {
                if (!Page.IsBusy)
                {
                    Page.IsBusy = true;
                    ProductDetailPage page = new ProductDetailPage()
                    {
                        BindingContext = new ProductDetailViewModel(productsSource, SelectedProduct)
                    };
                    await Page.Navigation.PushModalAsync(page);
                    SelectedProduct = null;
                    Page.IsBusy = false;
                }
            });
        }
    }
}
