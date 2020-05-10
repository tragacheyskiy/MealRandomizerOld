using MealRandomizer.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MealRandomizer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
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
                await Navigation.PushAsync(new ProductsPage(categoryVM.GetCategory()));
                categoriesCollectionView.SelectedItem = null;
            }
        });

        public CategoriesPage()
        {
            InitializeComponent();
            ProductCategoriesViewModel = new CategoriesVM();
            categoriesCollectionView.SelectionChangedCommand = SelectCategory;
        }
    }
}