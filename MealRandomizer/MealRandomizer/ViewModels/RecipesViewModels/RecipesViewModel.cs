using MealRandomizer.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels.RecipesViewModels
{
    public sealed class RecipesViewModel : BaseViewModel
    {
        public int TestProperty { get; }

        public RecipesViewModel()
        {
            TestProperty = 777;
        }
    }
}
