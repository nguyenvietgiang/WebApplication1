using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using WebApplication1.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        TimezoneResponseModelResponsePagination _timez = new TimezoneResponseModelResponsePagination();
        List<TimezoneResponseModelResponsePagination> _timezs = new List<TimezoneResponseModelResponsePagination>();

        string baseURL = "http://dev.s-erp.com.vn:9038/v1/";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task <IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("timezones");

                if(getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<TimezoneResponseModelResponsePagination>(result);

                    List<TimezoneResponseModel> listData = new List<TimezoneResponseModel>();
                    if (res != null && res.Code == 200)
                    {
                        listData.AddRange(res.Data.Content);
                    }

                    return View(listData);
                }
                else
                {
                    Console.WriteLine("Error calling web api");
                }    
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}