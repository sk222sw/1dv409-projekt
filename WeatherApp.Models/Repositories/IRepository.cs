using System.Collections.Generic;
using System.Linq;

namespace WeatherApp.Models.Repositories
{
    public interface IRepository
    {
        IEnumerable<Forecast> GetForecasts();
        Forecast GetForecastByCity(string city);
        void InsertForecast(Forecast forecast);
        void Save();
    }
}