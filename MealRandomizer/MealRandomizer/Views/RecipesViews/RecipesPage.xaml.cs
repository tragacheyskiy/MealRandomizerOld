using MealRandomizer.ViewModels.RecipesViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MealRandomizer.Views.RecipesViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipesPage : ContentPage
    {
        public RecipesPage()
        {
            InitializeComponent();
            BindingContext = new RecipesViewModel();
        }
    }
}