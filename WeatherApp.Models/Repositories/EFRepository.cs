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

        public IEnumerable<Location> GetLocations()
        {
            return _context.Locations.ToList();
        }

        public Location GetLocationByName(string location)
        {
            return (from item in _context.Locations
                     where item.Location1 == location
                     select item)
                     .AsQueryable()
                     .FirstOrDefault();
        }

        public void UpdateLocation(Location location)
        {
            if (_context.Entry(location).State == EntityState.Detached)
            {
                _context.Locations.Attach(location);
            }
            _context.Entry(location).State = EntityState.Modified;
        }

        public void DeleteLocation(Location location)
        {
            if (_context.Entry(location).State == EntityState.Detached)
            {
                _context.Locations.Attach(location);
            }
            _context.Locations.Remove(location); // changing entitystate to Deleted is done by the remove method
        }

        public void InsertLocation(Location location)
        {
            _context.Locations.Add(location);
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