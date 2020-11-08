﻿using MealRandomizer.Service;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels.ProductsViewModels
{
    public sealed class NewProductViewModel : BaseViewModel
    {
        public ProductEditViewModel ProductEditVM { get; }
        public Command CloseButtonCommand { get; }
        public Command AddButtonCommand { get; }

        public NewProductViewModel(CategoryViewModel currentCategory)
        {
            ProductEditVM = ProductEditViewModel.GetCategoryOnly(currentCategory);

            CloseButtonCommand = new Command(PopPageModal);

            AddButtonCommand = new Command(async () =>
            {
                if (!MainPage.IsBusy && ProductEditVM.IsInputCorrect)
                {
                    MainPage.IsBusy = true;
                    await ProductsData.Instance.AddProductAsync(ProductEditVM.NewProductVM);
                    MainPage.IsBusy = false;
                    PopPageModal();
                }
            });
        }
    }
}
