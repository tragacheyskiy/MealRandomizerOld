using MealRandomizer.Models;
using MealRandomizer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MealRandomizer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        public ProductsVM ProductsVM
        {
            get => BindingContext as ProductsVM;
            set => BindingContext = value;
        }

        public ProductsPage()
        {
            InitializeComponent();
            ProductsVM = new ProductsVM(ProductCategory.ALL);
        }

        public ProductsPage(ProductCategory category)
        {
            InitializeComponent();
            ProductsVM = new ProductsVM(category);
        }
    }
}