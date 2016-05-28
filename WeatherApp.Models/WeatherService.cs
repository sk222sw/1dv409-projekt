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

        public Forecast newForecast(string location)
        {
            // okay this got a bit messy :(
            var forecast = new Forecast();
            var weatherData = _webService.GetWeather(location);
            forecast.DayOneTemp = weatherData[0].Temperature;
            forecast.DayOneWeather = weatherData[0].Description;
            forecast.DayTwoTemp = weatherData[1].Temperature;
            forecast.DayTwoWeather = weatherData[1].Description;
            forecast.DayThreeTemp = weatherData[2].Temperature;
            forecast.DayThreeWeather = weatherData[2].Description;
            forecast.DayFourTemp = weatherData[3].Temperature;
            forecast.DayFourWeather = weatherData[3].Description;
            forecast.DayFiveTemp = weatherData[4].Temperature;
            forecast.DayFiveWeather = weatherData[4].Description;
            forecast.City = location;
            forecast.LastUpdate = DateTime.Now;
            return forecast;
        }

        public Forecast GetForecast(string location)
        {
            var forecast = _repository.GetForecastByCity(location);
            if (forecast == null)
            {
                forecast = newForecast(location);
                _repository.InsertForecast(forecast);
                _repository.Save();
            }
            else if (forecast.ShouldUpdate())
            {
                var fc = newForecast(location);

                forecast.DayOneTemp = fc.DayOneTemp;
                forecast.DayOneWeather = fc.DayOneWeather;
                forecast.DayTwoTemp = fc.DayTwoTemp;
                forecast.DayTwoWeather = fc.DayTwoWeather;
                forecast.DayThreeTemp = fc.DayThreeTemp;
                forecast.DayThreeWeather = fc.DayThreeWeather;
                forecast.DayFourTemp = fc.DayFourTemp;
                forecast.DayFourWeather = fc.DayFourWeather;
                forecast.DayFiveTemp = fc.DayFiveTemp;
                forecast.DayFiveWeather = fc.DayFiveWeather;
                forecast.LastUpdate = DateTime.Now;

                _repository.UpdateForecast(forecast);
                _repository.Save();
            }
            return forecast;
        }
    }
}