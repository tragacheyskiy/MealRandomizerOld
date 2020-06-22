using MealRandomizer.Service;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private const string BACK_ICON = "round_arrow_back_24.xml";
        private const string EDIT_ICON = "round_edit_24.xml";
        private const string CLOSE_ICON = "round_close_24";
        private const string CHECK_ICON = "round_check_24.xml";

        private bool isEntryReadOnly = true;
        private bool isEditing = false;
        private double opacity = 1.0;
        private ImageSource backButtonSource = BACK_ICON;
        private ImageSource editButtonSource = EDIT_ICON;
        private ProductViewModel currentProduct;
        private ProductsCollection ProductsSource { get; }

        public ProductViewModel CurrentProduct
        {
            get => currentProduct;
            private set => SetProperty(ref currentProduct, value);
        }
        public ProductEditViewModel ProductEditVM { get; }
        public bool IsEntryReadOnly
        {
            get => isEntryReadOnly;
            private set => SetProperty(ref isEntryReadOnly, value);
        }
        public bool IsEditing
        {
            get => isEditing;
            private set => SetProperty(ref isEditing, value);
        }
        public ImageSource BackButtonSource
        {
            get => backButtonSource;
            private set => SetProperty(ref backButtonSource, value);
        }
        public ImageSource EditButtonSource
        {
            get => editButtonSource;
            private set => SetProperty(ref editButtonSource, value);
        }
        public double Opacity
        {
            get => opacity;
            private set => SetProperty(ref opacity, value);
        }
        public Command BackButtonCommand { get; private set; }
        public Command DeleteButtonCommand { get; private set; }
        public Command EditButtonCommand { get; private set; }

        public ProductDetailViewModel(ProductsCollection productsSource, ProductViewModel selectedProduct)
        {
            ProductsSource = productsSource;
            CurrentProduct = selectedProduct;
            ProductEditVM = ProductEditViewModel.GetCategoryWithProductVM(ProductsSource.CurrentCategory, CurrentProduct);
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            DeleteButtonCommand = new Command(TryDeleteProduct);
            BackButtonCommand = new Command(async () =>
            {
                if (IsEditing)
                {
                    await CloseEditingState();
                    return;
                }
                await Page.Navigation.PopModalAsync();
            });
            EditButtonCommand = new Command(async () =>
            {
                if (IsEditing && ProductEditVM.IsInputCorrect)
                {
                    TryUpdateProduct();
                    return;
                }
                await OpenEditingState();
            });
        }

        private async void TryUpdateProduct()
        {
            bool isAccepted = await Page.DisplayAlert(null, $"Вы уверены, что хотите изменить {CurrentProduct.Name}?", "Изменить", "Отмена");
            bool isUpdated = false;
            if (isAccepted)
            {
                isUpdated = await ProductsSource.UpdateProductAsync(CurrentProduct, CurrentProduct = ProductEditVM.NewProductVM);
            }
            if (isUpdated)
            {
                await CloseEditingState();
            }
        }

        private async void TryDeleteProduct()
        {
            bool isAccepted = await Page.DisplayAlert(null, $"Вы уверены, что хотите удалить {CurrentProduct.Name}?", "Удалить", "Отмена");
            bool isDeleted = false;
            if (isAccepted)
            {
                isDeleted = await ProductsSource.DeleteProductAsync(CurrentProduct);
            }
            if (isDeleted)
            {
                await Page.Navigation.PopAsync();
            }
        }

        private Task CloseEditingState()
        {
            ProductEditVM.Refresh();
            IsEditing = false;
            IsEntryReadOnly = true;
            AnimateBackButton();
            return Task.CompletedTask;
        }

        private Task OpenEditingState()
        {
            IsEditing = true;
            IsEntryReadOnly = false;
            AnimateEditButton();
            return Task.CompletedTask;
        }

        private void AnimateEditButton()
        {
            ReduceOpacity();
            BackButtonSource = CLOSE_ICON;
            EditButtonSource = CHECK_ICON;
            IncreaseOpacity();
        }

        private void AnimateBackButton()
        {
            ReduceOpacity();
            BackButtonSource = BACK_ICON;
            EditButtonSource = EDIT_ICON;
            IncreaseOpacity();
        }

        private void ReduceOpacity()
        {
            for (int i = 0; i < 10; i++)
            {
                Opacity -= 0.1;
                Thread.Sleep(10);
            }
        }

        private void IncreaseOpacity()
        {
            for (int i = 0; i < 10; i++)
            {
                Opacity += 0.1;
                Thread.Sleep(10);
            }
        }
    }
}
