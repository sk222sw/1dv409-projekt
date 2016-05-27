using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherApp.Models.Repositories;
using WeatherApp.Models.WebServices;

namespace WeatherApp.Models
{
    public class WeatherService
    {
        private WeatherWebService _webService = new WeatherWebService();
        private IRepository _repository;

        public WeatherService()
            : this(new EFRepository())
        {

        }

        public WeatherService(IRepository repository)
        {
            _repository = repository;
        }

        public Forecast GetForecast(string location)
        {
            var forecast = _repository.GetForecastByCity(location);
            if (forecast == null)
            {
                // okay this got a bit messy :(
                forecast = new Forecast();
                var weatherList = _webService.GetWeather(location);
                forecast.DayOneTemp = weatherList[0].Temperature;
                forecast.DayOneWeather = weatherList[0].Description;
                forecast.DayTwoTemp = weatherList[1].Temperature;
                forecast.DayTwoWeather = weatherList[1].Description;
                forecast.DayThreeTemp = weatherList[2].Temperature;
                forecast.DayThreeWeather = weatherList[2].Description;
                forecast.DayFourTemp = weatherList[3].Temperature;
                forecast.DayFourWeather = weatherList[3].Description;
                forecast.DayFiveTemp = weatherList[4].Temperature;
                forecast.DayFiveWeather = weatherList[4].Description;
                forecast.City = location;
                forecast.LastUpdate = DateTime.Now;
                _repository.InsertForecast(forecast);
                _repository.Save();
            }
            return forecast;
        }
    }
}