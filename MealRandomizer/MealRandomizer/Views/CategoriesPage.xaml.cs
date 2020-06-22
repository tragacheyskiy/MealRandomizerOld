using MealRandomizer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MealRandomizer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        public CategoriesPage()
        {
            InitializeComponent();
            BindingContext = CategoriesViewModel.Instance;
        }
    }
}