using System;
using System.Collections.Generic;
using WhatsForDinner.Services;
using WhatsForDinner.ViewModels;
using Xamarin.Forms;

namespace WhatsForDinner.Views
{
    public partial class WhatsForDinnerPage : ContentPage
    {
        public WhatsForDinnerPage()
        {
            InitializeComponent();

            BindingContext = new WhatsForDinnerViewModel();
        }
    }
}
