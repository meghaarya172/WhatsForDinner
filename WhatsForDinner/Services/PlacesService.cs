using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WhatsForDinner.DataModels;

namespace WhatsForDinner.Services
{
    public class PlacesServices
    {
        // Constants
        private const string AK_00 = "AIzaSyCWdC6PdKv7nknQw";
        private const string AK_01 = "hiyuLqWqm8-WpkOwU8";

        // Class Members
        private PlacesInfo PlacesInfo;
        private bool Running;
        private List<Result> Results;

        // Constructor
        public PlacesServices(PlacesInfo info)
        {
            PlacesInfo = info;
            Results = new List<Result>();
        }

        // Public Methods
        public async Task<Result> GetRestaurant()
        {
            return await Task.FromResult(PickRestaurant());
        }

        // Private Methods

        private Result PickRestaurant()
        {
            GetNearbyPlaces();
            while (!Running)
            {
                // Waiting to get all results
                Thread.Sleep(1000);
            }
            // Get random index and return
            var generator = new Random();
            var index = generator.Next(Results.Count);
            return Results[index];
        }
        private async void GetNearbyPlaces()
        {
            try
            {
                Running = false;
                using (var client = new HttpClient())
                {
                    // First call will not contain a page token
                    var requestString = BuildRequestString();
                    while (!string.IsNullOrEmpty(requestString))
                    {
                        // Make call to Google Places API
                        var response = await client.GetStringAsync(requestString);
                        // Deserialize response
                        var result = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(response);

                        // Verify result is correct and results exist
                        if (result != null && result.status.Equals("OK") &&
                            result.results != null && result.results.Count > 0)
                        {
                            // Add each result to Results list
                            foreach (var place in result.results)
                            {
                                Results.Add(place);
                            }
                        }

                        if (!string.IsNullOrEmpty(result.next_page_token))
                        {
                            // Build Request string with page token
                            requestString = BuildRequestString(result.next_page_token);
                        }
                        else
                        {
                            // Exit loop
                            requestString = string.Empty;
                        }
                    }

                    Running = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private string BuildRequestString(string pageToken = "")
        {
            var requestString = string.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius={2}&type=restaurant", PlacesInfo.Location.Latitude, PlacesInfo.Location.Longitude, PlacesInfo.Range);
            if (!string.IsNullOrWhiteSpace(pageToken))
                requestString += string.Format("&pagetoken={0}", pageToken);
            return requestString + string.Format("&key={0}", (AK_00 + AK_01));
        }
    }
}
