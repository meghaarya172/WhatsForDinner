using System;
namespace WhatsForDinner.DataModels
{
    public class PlacesInfo
    {
        public GeoCoords Location { get; set; }
        public int Range { get; set; }
        public string Keyword { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }

        public PlacesInfo()
        {
            Location = new GeoCoords();
        }
    }
}
