using MealRandomizer.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MealRandomizer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductCategoriesViewPage : ContentPage
    {
        public CategoriesVM ProductCategoriesViewModel
        {
            get => BindingContext as CategoriesVM;
            set => BindingContext = value;
        }

        public ICommand SelectCategory => new Command(async () =>
        {
            if (categoriesCollectionView.SelectedItem is CategoryVM categoryVM)
            {
                await Navigation.PushAsync(new ProductsViewPage(categoryVM.GetCategory()));
                categoriesCollectionView.SelectedItem = null;
            }
        });

        public ProductCategoriesViewPage()
        {
            InitializeComponent();
            ProductCategoriesViewModel = new CategoriesVM();
            categoriesCollectionView.SelectionChangedCommand = SelectCategory;
        }
    }
}