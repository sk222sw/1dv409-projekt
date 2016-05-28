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
        [RegularExpression("^([a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]*$", ErrorMessage = "Enter a valid location.")]
        public string Location { get; set; }
        public int Temperature { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}