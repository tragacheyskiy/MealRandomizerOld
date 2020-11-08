using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected Page MainPage { get; } = Application.Current.MainPage;

        protected void PushPageModal(Page page)
        {
            OccupyPage(async () => await MainPage.Navigation.PushModalAsync(page));
        }

        protected void PopPageModal()
        {
            OccupyPage(async () => await MainPage.Navigation.PopModalAsync());
        }

        protected void PushPage(Page page)
        {
            OccupyPage(async () => await MainPage.Navigation.PushAsync(page));
        }

        protected void PopPage()
        {
            OccupyPage(async () => await MainPage.Navigation.PopAsync());
        }

        private void OccupyPage(Action action)
        {
            if (!MainPage.IsBusy)
            {
                MainPage.IsBusy = true;
                action?.Invoke();
                MainPage.IsBusy = false;
            }
        }

        protected void SetProperty<T>(ref T source, T value, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(source, value))
            {
                source = value;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
