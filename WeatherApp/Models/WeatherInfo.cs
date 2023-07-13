using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace WeatherApp.Models
{

    public class WeatherInfo
    {
        public class weather
        {
            public string description { get; set; }
            public string icon { get; set; }

        }
        public class main
        {
            public double temp { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }
        }
        public class wind
        {
            public double speed { get; set; }
        }
        public class sys
        {
            public string country { get; set; }
            public long sunrise { get; set; }
            public long sunset { get; set; }
        }
        public class root
        {
            public main main { get; set; }
            public List<weather> weather { get; set; }
            public wind wind { get; set; }
            public sys sys { get; set; }
            public string name { get; set; }
        }
    }
}
