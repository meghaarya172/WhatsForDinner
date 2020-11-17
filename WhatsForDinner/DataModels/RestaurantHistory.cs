using System;
using System.Collections.Generic;
using SQLite;
namespace WhatsForDinner.DataModels
{
    public class RestaurantHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PlaceId { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class HistoryGroup : List<RestaurantHistory>
    {
        public string Name { get; private set; }
        public HistoryGroup(string date, List<RestaurantHistory> histories) : base(histories)
        {
            Name = date;
        }
    }
}
