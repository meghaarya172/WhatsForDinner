using System;
using WhatsForDinner.DataModels;
using WhatsForDinner.Services;
using Xamarin.Forms;

namespace WhatsForDinner.ViewModels
{
    public class MapDetailViewModel : BaseViewModel
    {
        Result _restaurant;
        public Result Restaurant
        {
            get => _restaurant;
            set
            {
                _restaurant = value;
                OnPropertyChanged();
            }
        }
        public MapDetailViewModel(Result restaurant)
        {
            Restaurant = restaurant;
        }


        public Command ExitCommand =>
            new Command(() => App.Current.MainPage.Navigation.PopModalAsync());
    }
}
