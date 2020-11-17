using System;
using System.Collections.Generic;
using System.Diagnostics;
using WhatsForDinner.DataModels;

namespace WhatsForDinner.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        List<RestaurantHistory> _history;
        public List<RestaurantHistory> History
        {
            get => _history;
            set
            {
                _history = value;
                OnPropertyChanged();
            }
        }
        public HistoryViewModel()
        {
            GetHistory();
        }

        private async void GetHistory()
        {
            History = await App.Database.GetHistoryAsync();
        }
    }
}
