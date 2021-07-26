using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Weather.Models;
using System.Net;
using WeatherForecast.Models;

namespace Weather.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public ActionResult Weather()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public  String WeatherDetail(string city)
        {
            var key = "3688216704cb7500ea106049fb260af0";
            var url = string.Format($"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&cnt=1&appid={key}");
            using (WebClient client = new WebClient())
            {
                
                string json = client.DownloadString(url);

                RootObject res = JsonConvert.DeserializeObject<RootObject>(json);
                ResultViewModel rslt = new ResultViewModel();
                rslt.Country = res.sys.country;
                rslt.City = res.name;
                rslt.Lat = Convert.ToString(res.coord.lat);
                rslt.Lon = Convert.ToString(res.coord.lon);
                rslt.Description = res.weather[0].description;
                rslt.Humidity = Convert.ToString(res.main.humidity);
                rslt.Temp = Convert.ToString(res.main.temp);
                rslt.TempFeelsLike = Convert.ToString(res.main.feels_like);
                rslt.TempMax = Convert.ToString(res.main.temp_max);
                rslt.TempMin = Convert.ToString(res.main.temp_min);
                rslt.WeatherIcon = res.weather[0].icon;

                var jsonString = JsonConvert.SerializeObject(rslt);
                return jsonString;
            }
        }
     }
}
