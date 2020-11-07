using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using MealRandomizer.ViewModels.ProductsViewModels;
using MealRandomizer.Views.ProductsViews;
using System.Linq;

namespace MealRandomizer.Droid
{
    [Activity(Label = "MealRandomizer", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnBackPressed()
        {
            var currentPage = Xamarin.Forms.Application.Current.MainPage.Navigation.ModalStack.LastOrDefault() as ProductDetailPage;
            var productDetailVM = currentPage?.BindingContext as ProductDetailViewModel;
            if (productDetailVM != null && productDetailVM.IsEditing)
            {
                productDetailVM.BackButtonCommand.Execute(null);
                return;
            }
            base.OnBackPressed();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}