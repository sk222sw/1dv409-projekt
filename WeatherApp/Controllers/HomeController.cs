﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Models;
using WeatherApp.Models.Repositories;

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
        public ActionResult Index([Bind(Include = "Location")] Weather model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var service = new WeatherService();
                    Forecast forecast = service.GetForecast(model.Location);
                    return View("Forecast", forecast);
                }
                catch (DataException)
                {
                    TempData["error"] = "There was an error getting the forecast for " + model.Location + ", are you sure it's a real location?";
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