using System;
namespace WhatsForDinner.DataModels
{
    public class PlacesInfo
    {
        public GeoCoords Location { get; set; }
        public int Range { get; set; }

        public PlacesInfo()
        {
            //TODO: Default values for now
            Range = 1500;
            Location = new GeoCoords
            {
                Latitude = 33.007376,
                Longitude = -96.900484
            };
        }
    }
}
