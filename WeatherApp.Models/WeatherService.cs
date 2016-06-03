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
        public Location newLocation(string location)
        {
            var loc = new Location();
            loc.Location1 = location;
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
                    loc.Weathers.Add(weather);
                }
            }
            loc.LastUpdate = DateTime.Now;
            return loc;
        }

        public Location GetForecast(string location)
        {
            var loc = _repository.GetLocationByName(location);
            if (loc == null)
            {
                try
                {
                    loc = newLocation(location);

                    _repository.InsertLocation(loc);
                    _repository.Save();
                }
                catch (Exception)
                {
                    throw new DataException();
                }
            }
            else if (loc.ShouldUpdate())
            {
                _repository.UpdateLocation(loc);
                _repository.Save();
            }
            return loc;
        }
    }
}