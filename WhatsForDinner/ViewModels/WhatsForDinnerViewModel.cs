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

        private readonly string[] PriceRanges = { "$", "$$", "$$$", "$$$$" };

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
                Result restaurant = null;
                if (RangeIndex > -1)
                {
                    var range = Ranges[RangeIndex];

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
                            }

                            if (!string.IsNullOrEmpty(KeywordText))
                                placesInfo.Keyword = KeywordText;

                            if (MinPriceIndex > -1)
                                placesInfo.MinPrice = PriceRanges[MinPriceIndex].Length;

                            if (MaxPriceIndex > -1)
                                placesInfo.MaxPrice = PriceRanges[MaxPriceIndex].Length;

                            var service = new PlacesServices(placesInfo);
                            restaurant = await service.GetRestaurant();
                        }
                    });

                    await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new MapDetailPage(restaurant)));
                    IsBusy = false;
                }
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
            catch (FeatureNotSupportedException)
            {
                Debugger.Break();
                // Handle not supported on device exception
                //await Application.Current.MainPage.DisplayAlert("Alert", "not supported on device" + fnsEx.Message, "OK");
            }
            catch (FeatureNotEnabledException)
            {
                Debugger.Break();
                // Handle not enabled on device exception
                //await Application.Current.MainPage.DisplayAlert("Alert", "not enabled on device" + fneEx.Message, "OK");
            }
            catch (PermissionException)
            {
                Debugger.Break();
                // Handle permission exception
                //await Application.Current.MainPage.DisplayAlert("Alert", "permission exception" + pEx.Message, "OK");
            }
            catch (Exception)
            {
                Debugger.Break();
                // Unable to get location
                //await Application.Current.MainPage.DisplayAlert("Alert", "unable to get location" + ex.Message, "OK");
            }
            return null;
        }
    }
}
