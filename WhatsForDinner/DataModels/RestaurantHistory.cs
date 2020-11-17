using System;
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
}
