using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Models;
using WeatherApp.Models.Repositories;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;

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
        public ActionResult Index([Bind(Include = "Location1")] Location model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var service = new WeatherService();
                    var loc = service.GetForecast(model.Location1);
                    var vm = new ViewModel(loc);
                    return View("Forecast", vm);
                }
                catch (DataException)
                {
                    TempData["error"] = "There was an error getting the forecast for " + model.Location1 + ", are you sure it's a real location?";
                    return View();
                }
            }
            return View("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}