using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WeatherApp.Models.DataModels;

namespace WeatherApp.Models.Repositories
{
    public class EFRepository : IRepository
    {
        private WeatherAppEntities _context = new WeatherAppEntities();

        public IEnumerable<Forecast> GetForecasts()
        {
            return _context.Forecasts.ToList();
        }

        public Forecast GetForecastByCity(string city)
        {
            return (from item in _context.Forecasts
                     where item.City == city
                     select item)
                     .AsQueryable()
                     .FirstOrDefault();
        }

        public void UpdateForecast(Forecast forecast)
        {
            if (_context.Entry(forecast).State == EntityState.Detached)
            {
                _context.Forecasts.Attach(forecast);
            }
            _context.Entry(forecast).State = EntityState.Modified;
        }

        public void DeleteForecast(Forecast forecast)
        {
            if (_context.Entry(forecast).State == EntityState.Detached)
            {
                _context.Forecasts.Attach(forecast);
            }
            _context.Forecasts.Remove(forecast); // changing entitystate to Deleted is done by the remove method
        }

        public void InsertForecast(Forecast forecast)
        {
            _context.Forecasts.Add(forecast);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}