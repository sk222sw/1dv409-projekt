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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Location")] Weather model)
        {
            if (ModelState.IsValid)
            {
                var city = model.Location;

                var webService = new WeatherWebService();

                var forecast = _repository.GetForecastByCity(city);

                if (forecast == null)
                {
                    IEnumerable<Weather> weatherList = webService.GetWeather(city);
                }
                else
                {

                }

                return View("Forecast", forecast);

            }
            return View("Index");
        }

    }
}