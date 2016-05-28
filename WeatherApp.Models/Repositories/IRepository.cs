using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherApp.Models.Repositories
{
    public interface IRepository : IDisposable
    {
        IEnumerable<Forecast> GetForecasts();
        Forecast GetForecastByCity(string city);
        void InsertForecast(Forecast forecast);
        void UpdateForecast(Forecast forecast);
        void DeleteForecast(Forecast forecast);
        void Save();
    }
}