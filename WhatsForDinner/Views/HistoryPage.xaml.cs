using System;
using System.Collections.Generic;
using System.Diagnostics;
using WhatsForDinner.ViewModels;
using Xamarin.Forms;

namespace WhatsForDinner.Views
{
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
            BindingContext = new HistoryViewModel();
        }

    }
}
