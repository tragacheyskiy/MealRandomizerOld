using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MealRandomizer.ViewModels;
using System.Diagnostics;
using System.Windows.Input;

namespace MealRandomizer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductCategoriesViewPage : ContentPage
    {
        public ProductCategoriesVM ProductCategoriesViewModel
        {
            get => BindingContext as ProductCategoriesVM;
            set => BindingContext = value;
        }

        public ICommand SelectCategory => new Command(async () =>
        {
            if (categoriesCollectionView.SelectedItem is string name)
            {
                var category = Service.EnumParser.GetCategoryFromString(name);
                await Navigation.PushAsync(new ProductsViewPage(category));
                categoriesCollectionView.SelectedItem = null;
            }
        });

        public ProductCategoriesViewPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ProductCategoriesViewModel = new ProductCategoriesVM();
            categoriesCollectionView.SelectionChangedCommand = SelectCategory;
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is string name)
            {
                var category = Service.EnumParser.GetCategoryFromString(name);
                sender = null;
                Navigation.PushAsync(new ProductsViewPage(category));
            }
        }
    }
}