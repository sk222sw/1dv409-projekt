using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WeatherApp.Models.WebServices
{
    public class WeatherWebService
    {
        public void GetWeather(string city)
        {
            string rawJson;
                             
            var uriString = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&mode=json&appid=e14a7b8f35864e3466caaa2b1db332c2";
            var request = (HttpWebRequest)WebRequest.Create(uriString);

            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawJson = reader.ReadToEnd();
            }
        }
    }
}