using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherApp.Models
{
    // Föreläsning 4, ca 52:00 så pratar han om partial och det
    [MetadataType(typeof(Forecast_Metadata))]
    public partial class Forecast
    {
    }

    internal class Forecast_Metadata
    {
    }
}