using System;
using System.Threading;
using System.Threading.Tasks;
using WhatsForDinner.DataModels;
using WhatsForDinner.Services;
using WhatsForDinner.Views;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;
using System.Collections.Generic;

namespace WhatsForDinner.ViewModels
{
    public class WhatsForDinnerViewModel : BaseViewModel
    {
        // Properties
        private readonly int[] Ranges = { 1, 5, 10, 15, 20, 25 };
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

        // trying to mimic google min/max price scale $=1-10, $$=11-20, $$$=21-30, $$$$=31-500
        private readonly int[] MinPriceRanges = { 1, 11, 21, 31 };
        private readonly int[] MaxPriceRanges = { 10, 20, 30, 500 };

        int _maxPriceIndex;
        public int MaxPriceIndex
        {
            get => _maxPriceIndex;
            set
            {
                _maxPriceIndex = value;
                OnPropertyChanged();
            }
        }

        int _minPriceIndex;
        public int MinPriceIndex
        {
            get => _minPriceIndex;
            set
            {
                _minPriceIndex = value;
                OnPropertyChanged();
            }
        }

        string _keywordText;
        public string KeywordText
        {
            get => _keywordText;
            set
            {
                _keywordText = value;
                OnPropertyChanged();
            }
        }
        public const string KeywordBlank = "blank";

        // Constructors
        public WhatsForDinnerViewModel()
        {
            RangeIndex = -1;
            MinPriceIndex = -1;
            MaxPriceIndex = -1;
        }

        // Command
        public Command RestaurantCommand
            => new Command(PickRestaurant);
        async void PickRestaurant()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                var range = Ranges[RangeIndex];
                Result restaurant = null;

                var minPrice = MinPriceRanges[MinPriceIndex];
                var maxPrice = MaxPriceRanges[MaxPriceIndex];
                string keyword = KeywordText ?? KeywordBlank;

                var location = await GetLocation();

                await Task.Run(async () =>
                {
                    if (range > 0)
                    {
                        var toMeters = (int)Math.Round((double)(range) * 1609.34);
                        PlacesInfo placesInfo = new PlacesInfo
                        {
                            Range = toMeters
                        };

                        if (location != null)
                        {
                            placesInfo.Location.Latitude = location.Latitude;
                            placesInfo.Location.Longitude = location.Longitude;
                            placesInfo.MinPrice = minPrice;
                            placesInfo.MaxPrice = maxPrice;
                            placesInfo.Keyword = keyword;
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

        private async Task<Xamarin.Essentials.Location> GetLocation()
        {
            try
            {

                CancellationTokenSource cts;
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                if (location != null)
                {
                    return location;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Debugger.Break();
                // Handle not supported on device exception
                //await Application.Current.MainPage.DisplayAlert("Alert", "not supported on device" + fnsEx.Message, "OK");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                Debugger.Break();
                // Handle not enabled on device exception
                //await Application.Current.MainPage.DisplayAlert("Alert", "not enabled on device" + fneEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                Debugger.Break();
                // Handle permission exception
                //await Application.Current.MainPage.DisplayAlert("Alert", "permission exception" + pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                Debugger.Break();
                // Unable to get location
                //await Application.Current.MainPage.DisplayAlert("Alert", "unable to get location" + ex.Message, "OK");
            }
            return null;
        }
    }
}
