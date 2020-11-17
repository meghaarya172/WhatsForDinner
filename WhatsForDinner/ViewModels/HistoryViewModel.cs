using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WhatsForDinner.DataModels;

namespace WhatsForDinner.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        List<HistoryGroup> _history;
        public List<HistoryGroup> History
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
            var history = await App.Database.GetHistoryAsync();
            var dates = history.Select(x => x.DateTime.ToShortDateString()).Distinct().ToList();
            var groups = new List<HistoryGroup>();
            foreach (var date in dates)
            {
                var histories = history.Where(x => x.DateTime.ToShortDateString().Equals(date)).ToList();
                groups.Add(new HistoryGroup(date, histories));
            }
            History = groups;
        }
    }
}
