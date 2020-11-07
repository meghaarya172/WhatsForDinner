using System;
using System.Threading.Tasks;
using WhatsForDinner.DataModels;
using WhatsForDinner.Services;
using Xamarin.Forms;

namespace WhatsForDinner.ViewModels
{
    public class WhatsForDinnerViewModel : BaseViewModel
    {
        // Properties
        private readonly int[] Ranges = { 5, 10, 15, 20, 25 };
        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        int _rangeIndex;
        public int RangeIndex
        {
            get => _rangeIndex;
            set
            {
                _rangeIndex = value;
                OnPropertyChanged();
            }
        }

        // Constructors
        public WhatsForDinnerViewModel()
        {
            RangeIndex = -1;
        }

        // Command
        public Command RestaurantCommand
            => new Command(PickRestaurant);
        async void PickRestaurant()
        {
            if (!IsBusy)
            {
                var range = Ranges[RangeIndex];
                Result restaurant = null;
                await Task.Run(async () =>
                {
                    if (range > 0)
                    {
                        IsBusy = true;
                        // TODO: Convert range (Miles) into meters
                        //TODO: Get Device's current location
                        var service = new PlacesServices(new PlacesInfo());
                        restaurant = await service.GetRestaurant();
                        IsBusy = false;
                    }
                });
                await Application.Current.MainPage.DisplayAlert("What's for Dinner?", string.Format("You should eat at the \"{0}\"", restaurant.name), "Ok");
            }
        }
    }
}
