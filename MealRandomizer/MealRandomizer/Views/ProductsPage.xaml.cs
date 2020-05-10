using MealRandomizer.Models;
using MealRandomizer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MealRandomizer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsViewPage : ContentPage
    {
        public ProductsVM ProductsVM
        {
            get => BindingContext as ProductsVM;
            set => BindingContext = value;
        }

        public ProductsViewPage()
        {
            InitializeComponent();
            ProductsVM = new ProductsVM(ProductCategory.ALL);
        }

        public ProductsViewPage(ProductCategory category)
        {
            InitializeComponent();
            ProductsVM = new ProductsVM(category);
        }
    }
}