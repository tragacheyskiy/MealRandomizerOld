using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MealRandomizer.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected Page Page { get; } = Application.Current.MainPage;

        public event PropertyChangedEventHandler PropertyChanged;

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
