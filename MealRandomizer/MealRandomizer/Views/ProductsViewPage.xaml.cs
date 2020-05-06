using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MealRandomizer.ViewModels;
using MealRandomizer.Models;

namespace MealRandomizer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsViewPage : ContentPage
    {
        private readonly ProductCategory category;

        public ProductsVM ProductsVM
        {
            get => BindingContext as ProductsVM;
            set => BindingContext = value;
        }

        public ProductsViewPage()
        {
            InitializeComponent();
        }

        public ProductsViewPage(ProductCategory category)
        {
            InitializeComponent();
            this.category = category;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ProductsVM = new ProductsVM(category);
        }
    }
}