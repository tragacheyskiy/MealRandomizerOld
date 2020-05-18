using MealRandomizer.ViewModels;
using System;
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
            if (!IsBusy && categoriesCollectionView.SelectedItem is CategoryVM categoryVM)
            {
                IsBusy = true;
                var page = new ProductsPage(new ProductsVM(categoryVM));
                await Navigation.PushAsync(page);
                categoriesCollectionView.SelectedItem = null;
                IsBusy = false;
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