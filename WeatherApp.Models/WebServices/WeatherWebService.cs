using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace WeatherApp.Models.WebServices
{
    public class WeatherWebService
    {
        public Weather[] GetWeather(string city)
        {
            var xml = String.Empty;
            var uriString = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&mode=xml&appid=e14a7b8f35864e3466caaa2b1db332c2";
            var request = (HttpWebRequest)WebRequest.Create(uriString);

            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                xml = reader.ReadToEnd();
            }



            var doc = XDocument.Parse(xml);
            //var doc = XDocument.Load("C:/Users/Sonny/Documents/visual studio 2015/Projects/WeatherApp/WeatherApp/App_Data/data.xml");
            var model = (from time in doc.Descendants("time")
                         .Where(t => DateTime.Parse(t.Attribute("from").Value).Hour == 15)
                         select new Weather
                         {
                             Date = DateTime.Parse(time.Attribute("from").Value),
                             Temperature = Convert.ToInt32(double.Parse(time.Element("temperature").Attribute("value").Value, CultureInfo.InvariantCulture)),
                             Description = time.Element("symbol").Attribute("var").Value
                         })
                         .ToArray();

            return model;
        }
    }
}