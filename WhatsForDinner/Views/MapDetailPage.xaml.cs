using System;
using System.Collections.Generic;
using WhatsForDinner.DataModels;
using WhatsForDinner.Services;
using WhatsForDinner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WhatsForDinner.Views
{
    public partial class MapDetailPage : ContentPage
    {
        MapDetailViewModel ViewModel => BindingContext as MapDetailViewModel;
        public MapDetailPage(Result result)
        {
            InitializeComponent();

            BindingContext = new MapDetailViewModel(result);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(ViewModel.Restaurant.geometry.location.lat,
                    ViewModel.Restaurant.geometry.location.lng), Distance.FromMiles(.1)));

            map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = ViewModel.Restaurant.name,
                Position = new Position(ViewModel.Restaurant.geometry.location.lat, ViewModel.Restaurant.geometry.location.lng)
            });
        }

    }
}
