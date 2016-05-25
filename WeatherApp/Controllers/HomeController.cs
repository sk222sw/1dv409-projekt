using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WeatherApp.Models;
using WeatherApp.Models.DataModels;
using WeatherApp.Models.WebServices;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private WeatherAppEntities _context = new WeatherAppEntities();

        public ActionResult Index()
        {
            var xml = String.Empty;
            var city = "Kalmar";
            var uriString = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&mode=xml&appid=e14a7b8f35864e3466caaa2b1db332c2";
            var request = (HttpWebRequest)WebRequest.Create(uriString);

            //var fitta = "4.4";
            //var d = double.Parse(fitta, CultureInfo.InvariantCulture);
            //var i = Convert.ToInt32(d);
            //var hej = Convert.ToInt32(double.Parse(time.Element("temperature").Attribute("value").Value, CultureInfo.InvariantCulture));
            //var h = String.Empty;

            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                xml = reader.ReadToEnd();
            }

            var doc = XDocument.Parse(xml);
            var model = (from time in doc.Descendants("time")
                         .Where(t => DateTime.Parse(t.Attribute("from").Value).Hour == 00)
                         select new Weather
                         {
                             Date = DateTime.Parse(time.Attribute("from").Value),
                             Temperature = Convert.ToInt32(double.Parse(time.Element("temperature").Attribute("value").Value, CultureInfo.InvariantCulture)),
                             Description = time.Element("symbol").Attribute("var").Value
                         })
                         .ToList();


            return View(model);
        }
    }
}

//Temperature = Decimal.Round((decimal.Parse(time.Element("temperature").Value)))
