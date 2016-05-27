using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherApp.Models.DataModels;

namespace WeatherApp.Models.Repositories
{
    public class EFRepository : IRepository, IDisposable
    {
        private WeatherAppEntities _context = new WeatherAppEntities();

        public IEnumerable<Forecast> GetForecasts()
        {
            return _context.Forecasts.ToList();
        }

        public Forecast GetForecastByCity(string city)
        {
            var f = (from item in _context.Forecasts
                     where item.City == city
                     select item).AsQueryable().FirstOrDefault();
            return f;
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

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EFRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}