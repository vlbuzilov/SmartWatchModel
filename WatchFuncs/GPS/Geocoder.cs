using System.Net.Http;
using Newtonsoft.Json;
using Mono.Web;


namespace OOP_Lab6.WatchFuncs
{
    public class Geocoder
    {
        //API's URL
        //=================================================================================================================================
        private const string _baseUrl = "https://maps.googleapis.com/maps/api/geocode/json";
        //=================================================================================================================================
        
        //methods
        //=================================================================================================================================
        public (double Latitude, double Longitude) GeocodeAddress(string address, string apiKey)
        {
            var httpClient = new HttpClient();
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["address"] = address;
            query["key"] = apiKey;
            var url = $"{_baseUrl}?{query}";
            var response = httpClient.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<GeocodeResult>(json);
                if (result.Status == "OK")
                {
                    var location = result.Results[0].Geometry.Location;
                    return (location.Lat, location.Lng);
                }
            }
            return default;
        }
        //=================================================================================================================================

        //classes
        //=================================================================================================================================
        private class GeocodeResult
        {
            public string Status { get; set; }
            public Result[] Results { get; set; }
        }

        private class Result
        {
            public Geometry Geometry { get; set; }
        }

        private class Geometry
        {
            public Location Location { get; set; }
        }

        private class Location
        {
            public double Lat { get; set; }
            public double Lng { get; set; }
        }
        //=================================================================================================================================
    }
}