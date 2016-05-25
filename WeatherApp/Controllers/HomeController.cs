using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private WeatherEntities _context = new WeatherEntities();

        public ActionResult Index()
        {
            var model = _context.Forecasts.ToList();
            //var weather = new Models.WebServices.WeatherWebService();
            //weather.GetWeather("kalmar");
            //var test = new Test();
            return View(model);
        }
    }
}