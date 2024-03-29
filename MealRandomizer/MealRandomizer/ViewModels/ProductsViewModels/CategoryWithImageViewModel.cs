﻿using Xamarin.Forms;

namespace MealRandomizer.ViewModels.ProductsViewModels
{
    public class CategoryWithImageViewModel
    {
        public CategoryViewModel CategoryVM { get; }
        public ImageSource Image { get; }

        public CategoryWithImageViewModel(CategoryViewModel categoryVM, ImageSource image)
        {
            CategoryVM = categoryVM;
            Image = image;
        }
    }
}
