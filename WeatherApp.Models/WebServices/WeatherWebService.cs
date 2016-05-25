using System;
using System.Collections.Generic;
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
        public void GetWeather(string city)
        {
            string xml;



            //string nummer = "5.5";
            //double dec = Double.Parse(nummer);
            ////var flo = Math.Round(dec);
            ////int inn = Int32.Parse(flo);
            //int i = Convert.ToInt32(dec);

            var uriString = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&mode=xml&appid=e14a7b8f35864e3466caaa2b1db332c2";
            var request = (HttpWebRequest)WebRequest.Create(uriString);

            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                xml = reader.ReadToEnd();
            }
            var parsedXml = XDocument.Parse(xml);

            var weather = (from time in parsedXml.Descendants("time")
                           select new Weather
                           {
                               Date = DateTime.Parse(time.Element("from").ToString()),
                               //Temperature = Convert.ToInt32(Double.Parse(time.Element("temperature").Value))
                           }).ToList();

        }
    }
}