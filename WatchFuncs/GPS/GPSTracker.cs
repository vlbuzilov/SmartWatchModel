using System;
using CoreLocation;

namespace OOP_Lab6.WatchFuncs
{
    public class GPSTracker
    {
        //fields
        //=================================================================================================================================
        private double latitude;
        private double longitude;
        //=================================================================================================================================
        
        //properties
        //=================================================================================================================================
        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }
        //=================================================================================================================================
        
        //constructors
        //=================================================================================================================================
        public GPSTracker()
        {
            latitude = 0;                                 //default coordinates (could be anything value)
            longitude = 0;
        }
        
        public GPSTracker(double latitude, double longitude)    //collect coordinates from device
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }
        //=================================================================================================================================

        //methods
        //================================================================================================================================= 
        public (double latitude, double longitude) GetCoordinates(string address)
        {
            var geocoder = new Geocoder();
            var apiKey = "AIzaSyCnlvP1TYLUPnWsQ0fWSQrM1VvVztx96sU";
            var (latitude, longitude) =  geocoder.GeocodeAddress(address, apiKey);
            return (latitude, longitude);
        }
        public string GetLastCoordinates()  //position output
        {
            return $"Latitude: {latitude}, Longitude: {longitude}";
        }
        
        public double GetDistance(double lat2, double lon2)
        {
            try
            {
                const double R = 6371; // Earth's radius in km
                double lat1 = latitude;
                double lon1 = longitude;

                double dLat = (lat2 - lat1) * Math.PI / 180;
                double dLon = (lon2 - lon1) * Math.PI / 180;

                double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                           Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                           Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a)); 
                double dist = R * c;

                return dist;
            }
            catch (Exception e)
            {
                CreateLog.log.Error($"Error occurred in GetDistance method, in GPSTracker: {e.Message}");
                Console.WriteLine(e.Message);
                return double.MaxValue; // return a large value instead of 0 to indicate an error
            }
        }   
        
        public DateTime GetETA(double lat2, double lon2, double speed)
        {
            double distance = GetDistance(lat2, lon2);
            int hours = (int)(distance / speed);
            DateTime dateTimeNow = DateTime.Now;
            dateTimeNow = dateTimeNow.AddHours(hours);
            return dateTimeNow;
        }

        public string GetDestination(double lat2, double lon2)
        {
            string dest = "";
            try
            {
                double bearing = GetBearing(lat2, lon2);
                if (bearing >= 337.5 || bearing < 22.5)
                {
                    dest = "Move north";
                }
                else if (bearing >= 22.5 && bearing < 67.5)
                {
                    dest = "Move northeast";
                }
                else if (bearing >= 67.5 && bearing < 112.5)
                {
                    dest = "Move east";
                }
                else if (bearing >= 112.5 && bearing < 157.5)
                {
                    dest = "Move southeast";
                }
                else if (bearing >= 157.5 && bearing < 202.5)
                {
                    dest = "Move south";
                }
                else if (bearing >= 202.5 && bearing < 247.5)
                {
                    dest = "Move southwest";
                }
                else if (bearing >= 247.5 && bearing < 292.5)
                {
                    dest = "Move west";
                }
                else if (bearing >= 292.5 && bearing < 337.5)
                {
                    dest = "Move northwest";
                }
            }
            catch (Exception e)
            {
                CreateLog.log.Error($"Error occurred in GetDestination method, in GPSTracker: {e.Message}");
                Console.WriteLine(e.Message);
            }

            return dest;
        }
        
        private double GetBearing(double lat2, double lon2)
        {
            double bearing;
            try
            {
                double dLon = (lon2 - longitude) * Math.PI / 180;
                double y = Math.Sin(dLon) * Math.Cos(lat2 * Math.PI / 180);
                double x = Math.Cos(latitude * Math.PI / 180) * Math.Sin(lat2 * Math.PI / 180) -
                           Math.Sin(latitude * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) * Math.Cos(dLon);
                bearing = Math.Atan2(y, x) * 180 / Math.PI;
            }
            catch (Exception e)
            {
                CreateLog.log.Error($"Error occurred in GetBearing method, in GPSTracker: {e.Message}");
                Console.WriteLine(e.Message);
                throw;
            }

            return bearing;
        }
        
        public bool IsWithinRadius(double lat2, double lon2, double radius)
        {
            double distance = GetDistance(lat2, lon2);

            return distance <= radius;
        }
        //=================================================================================================================================
    }
}