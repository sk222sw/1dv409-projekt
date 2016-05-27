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
using WeatherApp.Models.Repositories;
using WeatherApp.Models.WebServices;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private WeatherAppEntities _context = new WeatherAppEntities();
        private IRepository _repository = new EFRepository();

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var city = "Kalmar";

            var webService = new WeatherWebService();

            // kolla om City finns i databasen
            var forecast = _repository.GetForecastByCity(city);
            //var forecasts = _repository.GetForecasts();

            IEnumerable<Weather> weatherList = webService.GetWeather(city);


            return View(weatherList);
        }
    }
}