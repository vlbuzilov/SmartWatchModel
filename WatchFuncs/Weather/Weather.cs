using System;
using System.IO;
using System.Net;
using Json.Net;
using Newtonsoft.Json;

namespace OOP_Lab6.WatchFuncs
{
    public class Weather
    {
        //API's URL
        //=================================================================================================================================
        private const string url = "https://api.openweathermap.org/data/2.5/weather?q=Kyiv&units=metric&appid=5dfe3583e560ba2191440fabfe4a0b86";

        private static HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        private HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //=================================================================================================================================
        
        //methods
        //=================================================================================================================================
        public void GetWeather()
        {
            try
            {
                string response;
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
                Console.WriteLine($"Temperature in {weatherResponse.Name}: {weatherResponse.Main.Temp}C");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //=================================================================================================================================
    }
}