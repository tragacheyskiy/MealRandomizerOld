using MealRandomizer.Service;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels
{
    public class NewProductViewModel : BaseViewModel
    {
        private ProductsCollection ProductsSource { get; }

        public ProductEditViewModel ProductEditVM { get; }
        public Command CloseButtonCommand { get; private set; }
        public Command AddButtonCommand { get; private set; }

        public NewProductViewModel(ProductsCollection productsSource)
        {
            ProductsSource = productsSource;
            ProductEditVM = ProductEditViewModel.GetCategoryOnly(ProductsSource.CurrentCategory);
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            CloseButtonCommand = new Command(async () => await Page.Navigation.PopModalAsync());
            AddButtonCommand = new Command(async () =>
            {
                if (!Page.IsBusy && ProductEditVM.IsInputCorrect)
                {
                    Page.IsBusy = true;
                    await ProductsSource.AddProductAsync(ProductEditVM.NewProductVM);
                    await Page.Navigation.PopModalAsync();
                    Page.IsBusy = false;
                }
            });
        }
    }
}
