using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WeatherApp.Models
{
    public class WeatherModel
    {
        public async Task<WeatherInfo.root> Search(string city)
        {
            try
            {
                string apiKey = "5432e26ebb1a58b83fdf3710125975de";
                string apiUrlCurr = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={apiKey}";
                HttpWebRequest requestCurr = WebRequest.CreateHttp(apiUrlCurr);
                requestCurr.Method = "GET";

                using (WebResponse response = await requestCurr.GetResponseAsync())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string jsonResponse = reader.ReadToEnd();
                            WeatherInfo.root weatherInfo = JsonConvert.DeserializeObject<WeatherInfo.root>(jsonResponse);
                            return weatherInfo;
                        }
                    }
                }
            }catch(Exception exeption)
            {
                MessageBox.Show(exeption.Message);
                return null;
            }

        }
    }
}
