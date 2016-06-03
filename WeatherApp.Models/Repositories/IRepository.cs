using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherApp.Models.Repositories
{
    public interface IRepository : IDisposable
    {
        IEnumerable<Location> GetLocations();
        Location GetLocationByName(string location);
        void InsertLocation(Location location);
        void UpdateLocation(Location location);
        void DeleteLocation(Location location);
        void Save();
    }
}