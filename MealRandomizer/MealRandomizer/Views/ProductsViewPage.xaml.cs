using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealRandomizer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MealRandomizer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsViewPage : ContentPage
    {
        private ProductsVM productsVM;

        public ProductsViewPage()
        {
            InitializeComponent();
            BindingContext = productsVM = new ProductsVM();
        }
    }
}