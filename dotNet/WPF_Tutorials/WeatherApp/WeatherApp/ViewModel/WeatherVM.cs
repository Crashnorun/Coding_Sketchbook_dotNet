using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel
{
    public class WeatherVM
    {
        public AccuWeather Weather { get; set; }

        public WeatherVM()
        {
            Weather = new AccuWeather();
        }
    }
}

