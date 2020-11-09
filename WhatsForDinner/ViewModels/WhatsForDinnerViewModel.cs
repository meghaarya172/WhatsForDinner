using System;
using System.Threading;
using System.Threading.Tasks;
using WhatsForDinner.DataModels;
using WhatsForDinner.Services;
using WhatsForDinner.Views;
using Xamarin.Forms;
using Xamarin.Essentials;

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
                        
                        PlacesInfo placesInfo = new PlacesInfo();

                        try
                        {
                            CancellationTokenSource cts;
                            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                            cts = new CancellationTokenSource();
                            var location = await Geolocation.GetLocationAsync(request, cts.Token);

                            if (location != null)
                            {
                                placesInfo.Location.Latitude = location.Latitude;
                                placesInfo.Location.Longitude = location.Longitude;
                            }
                        }
                        catch (FeatureNotSupportedException fnsEx)
                        {
                            // Handle not supported on device exception
                            //await Application.Current.MainPage.DisplayAlert("Alert", "not supported on device" + fnsEx.Message, "OK");
                        }
                        catch (FeatureNotEnabledException fneEx)
                        {
                            // Handle not enabled on device exception
                            //await Application.Current.MainPage.DisplayAlert("Alert", "not enabled on device" + fneEx.Message, "OK");
                        }
                        catch (PermissionException pEx)
                        {
                            // Handle permission exception
                            //await Application.Current.MainPage.DisplayAlert("Alert", "permission exception" + pEx.Message, "OK");
                        }
                        catch (Exception ex)
                        {
                            // Unable to get location
                            //await Application.Current.MainPage.DisplayAlert("Alert", "unable to get location" + ex.Message, "OK");
                        }
                        
                        var service = new PlacesServices(placesInfo);
                        restaurant = await service.GetRestaurant();
                    }
                });
                //await Application.Current.MainPage.DisplayAlert("What's for Dinner?", string.Format("You should eat at the \"{0}\"", restaurant.name), "Ok");
                await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new MapDetailPage(restaurant)));
                IsBusy = false;
            }
        }
    }
}
