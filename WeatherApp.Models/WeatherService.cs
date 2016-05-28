using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WeatherApp.Models.Repositories;
using WeatherApp.Models.WebServices;

namespace WeatherApp.Models
{
    public enum ApiHour
    {
        twelveAM = 00,
        threeAM = 03,
        sixAM = 06,
        nineAM = 09,
        twelvePM = 12,
        threePM = 15,
        sixPM = 18,
        ninePM = 21
    }

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

        // note: weatherList will contain six entries if the time has passed the hour value, but only five entries are used
        public Forecast newForecast(string location)
        {
            var forecast = new Forecast();
            var weatherData = _webService.GetWeather(location);
            if (weatherData == null)
            {
                return null;
            }
            List<Weather> weatherList = new List<Weather>();
            int hour = Convert.ToInt32(ApiHour.threePM);

            // if the time passed the selected hour the first entry will be the first weather data from the api
            if (weatherData[0].Date.Hour > hour)
            {
                weatherList.Add(weatherData[0]);
            }

            foreach (Weather weather in weatherData)
            {
                if (weather.Date.Hour == hour)
                {
                    weatherList.Add(weather);
                }
            }

            // Should change the database to have forecast and weather tables so i can make this dynamic
            forecast.DayOneTemp = weatherList[0].Temperature;
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
                try
                {
                    forecast = newForecast(location);
                    _repository.InsertForecast(forecast);
                    _repository.Save();
                }
                catch (Exception)
                {
                    throw new DataException();
                }
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