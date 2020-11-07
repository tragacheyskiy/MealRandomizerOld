using MealRandomizer.Service;
using System.Diagnostics;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels.ProductsViewModels
{
    public class NewProductViewModel : BaseViewModel
    {
        public ProductEditViewModel ProductEditVM { get; }
        public Command CloseButtonCommand { get; private set; }
        public Command AddButtonCommand { get; private set; }

        public NewProductViewModel(CategoryViewModel currentCategory)
        {
            ProductEditVM = ProductEditViewModel.GetCategoryOnly(currentCategory);
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
                    await ProductsData.Instance.AddProductAsync(ProductEditVM.NewProductVM);
                    await Page.Navigation.PopModalAsync();
                    Page.IsBusy = false;
                }
            });
        }
    }
}
