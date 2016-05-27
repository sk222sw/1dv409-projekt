using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherApp.Models
{
    public class Weather
    {
        [DisplayName("Location")]
        [Required(ErrorMessage = "Enter a location")]
        public string Location { get; set; }
        public int Temperature { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}