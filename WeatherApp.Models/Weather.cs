using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherApp.Models
{
    [MetadataType(typeof(Weather_MetaData))]
    public partial class Weather
    {
    }

    internal class Weather_MetaData
    {
        public int Temperature { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}