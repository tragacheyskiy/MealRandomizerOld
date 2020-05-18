using MealRandomizer.Models;
using MealRandomizer.ViewModels;
using System.Windows.Input;
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

        public ICommand BackButtonCommand => new Command(async () =>
        {
            if (Navigation.NavigationStack.Count > 1)
            {
                await Navigation.PopAsync();
            }
        });

        public ICommand AddButtonCommand => new Command(async () =>
        {
            if (!IsBusy)
            {
                IsBusy = true;
                await Navigation.PushModalAsync(new NewProductPage(ProductsVM.CategoryVM.GetCategory()));
                IsBusy = false;
            }
        });

        public ProductsPage()
        {
            InitializeComponent();
            ProductsVM = new ProductsVM(new CategoryVM(ProductCategory.ALL,
                ImageSource.FromResource("MealRandomizer.ProductCategoryPics.chocolate.jpg")));
            BackButton.Command = BackButtonCommand;
            AddButton.Command = AddButtonCommand;
        }

        public ProductsPage(ProductsVM productsVM)
        {
            InitializeComponent();
            ProductsVM = productsVM;
            BackButton.Command = BackButtonCommand;
            AddButton.Command = AddButtonCommand;
        }
    }
}