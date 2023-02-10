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

        TimezoneMasterResponseModel _timez = new TimezoneMasterResponseModel();
        List<TimezoneMasterResponseModel> _timezs = new List<TimezoneMasterResponseModel>();

        string baseURL = "http://dev.s-erp.com.vn:9038/v1/";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task <IActionResult> Index(int? page)
        {
            if (page == null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                  
                        HttpResponseMessage getData = await client.GetAsync("timezones");
                    if (getData.IsSuccessStatusCode)
                    {
                        string result = getData.Content.ReadAsStringAsync().Result;
                        var res = JsonConvert.DeserializeObject<TimezoneMasterResponseModel>(result);
                        TimezoneResponsePaginationModel resPagi = new TimezoneResponsePaginationModel();
                        if (res != null && res.Code == 200)
                        {
                            resPagi.CurrentPage = res.Data.CurrentPage;
                            resPagi.TotalPages = res.Data.TotalPages;
                            resPagi.PageSize = res.Data.PageSize;
                            resPagi.NumberOfRecords = res.Data.NumberOfRecords;
                            resPagi.TotalRecords = res.Data.TotalRecords;
                            resPagi.Content = res.Data.Content;
                        }
                        return View(resPagi);
                    }
                    else
                    {
                        Console.WriteLine("Error calling web api");
                    }
                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage getData = await client.GetAsync("timezones?page="+page);

                    if (getData.IsSuccessStatusCode)
                    {
                        string result = getData.Content.ReadAsStringAsync().Result;
                        var res = JsonConvert.DeserializeObject<TimezoneMasterResponseModel>(result);
                        TimezoneResponsePaginationModel resPagi = new TimezoneResponsePaginationModel();
                        if (res != null && res.Code == 200)
                        {
                            resPagi.CurrentPage = res.Data.CurrentPage;
                            resPagi.TotalPages = res.Data.TotalPages;
                            resPagi.PageSize = res.Data.PageSize;
                            resPagi.NumberOfRecords = res.Data.NumberOfRecords;
                            resPagi.TotalRecords = res.Data.TotalRecords;
                            resPagi.Content = res.Data.Content;
                        }
                        return View(resPagi);
                    }
                    else
                    {
                        Console.WriteLine("Error calling web api");
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> DetailTimeZone(string? code)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("timezones/"+code);

                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<TimezoneResponseModel>(result);
                    return View(res);
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