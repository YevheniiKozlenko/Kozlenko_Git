using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{

    internal class WeatherVievModel : ViewModelBase
    {
        WeatherModel weather;

        public DateTime Now { get; set; }
        public WeatherVievModel()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, args) =>
            {
                Now = DateTime.Now;
            };
            timer.Start();
            weather = new WeatherModel();
        }

        public string cityName { get; set; }
        public string Temp { get; set; }
        public string Wind { get; set; }
        public string Humidity { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public BitmapImage Icon { get; set; }

        public ICommand Search
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var weatherData = await weather.Search(cityName);
                    DisplayWeatherInfo(weatherData);
                });
            }
        }

        private void DisplayWeatherInfo(WeatherInfo.root weatherInfo)
        {
            if (weatherInfo != null)
            {
                Description = weatherInfo.weather[0].description;
                Temp = weatherInfo.main.temp.ToString() + " °C";
                Wind = weatherInfo.wind.speed.ToString() + " м/с";
                Humidity = weatherInfo.main.humidity.ToString() + " %";
                Sunrise = ConvertTime(weatherInfo.sys.sunrise).ToShortTimeString();
                Sunset = ConvertTime(weatherInfo.sys.sunset).ToShortTimeString();
                Location = weatherInfo.name + ", " + weatherInfo.sys.country;

                BitmapImage weatherIcon = new BitmapImage();
                weatherIcon.BeginInit();
                weatherIcon.UriSource = new Uri($"http://openweathermap.org/img/wn/" +
                    $"{weatherInfo.weather[0].icon}@2x.png");
                weatherIcon.EndInit();
                Icon = weatherIcon;
            }
        }
        DateTime ConvertTime(long milliseconds)
        {
            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
            time = time.AddSeconds(milliseconds);
            return time;
        }
    }
}
