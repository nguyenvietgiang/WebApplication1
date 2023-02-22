using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using WebApplication1.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using Microsoft.AspNetCore.DataProtection.KeyManagement;


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

        public async Task<IActionResult> Index(TimezoneQueryModel model)
        {
            var getFilterUrl = "timezones?page=1&size=73";
            if (!string.IsNullOrEmpty(model.FullTextSearch))
            {
                getFilterUrl = getFilterUrl + "&filter=[key]";
                var fullTextsearchjson = JsonConvert.SerializeObject(model);
                getFilterUrl = getFilterUrl.Replace("[key]", fullTextsearchjson);

            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ4LXVzZXJOYW1lIjoic29uZG4iLCJ4LWZ1bGxOYW1lIjoiU29uIERvIiwieC11c2VySWQiOiJmZTAwMDAwMC0yMDEwLTIwMTAtMjAxMC0wMDAwMDAwMDAwMDEiLCJ4LWFwcElkIjoiMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwIiwieC1leHAiOiIxNjc5MzkyMjUxMzYzIiwieC1pYXQiOiIxNjc2OTczMDUxMzYzIiwicy1zdXBlcnVzZXIiOiJUcnVlIiwieC1yb2xlIjoiW10iLCJ4LXJpZ2h0IjoiW10iLCJleHAiOjE2NzkzNjcwNTEsImlzcyI6IlMtRVJQIiwiYXVkIjoiUy1FUlAifQ.UeAI9WpxJORCvgX2d53AlD6WHZLspK0IX5i6aGONjgM");
                HttpResponseMessage getData = await client.GetAsync(getFilterUrl);
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

            return View();
        }

        public async Task<IActionResult> DetailTimeZone(string? code)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ4LXVzZXJOYW1lIjoic29uZG4iLCJ4LWZ1bGxOYW1lIjoiU29uIERvIiwieC11c2VySWQiOiJmZTAwMDAwMC0yMDEwLTIwMTAtMjAxMC0wMDAwMDAwMDAwMDEiLCJ4LWFwcElkIjoiMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwIiwieC1leHAiOiIxNjc5MzkyMjUxMzYzIiwieC1pYXQiOiIxNjc2OTczMDUxMzYzIiwicy1zdXBlcnVzZXIiOiJUcnVlIiwieC1yb2xlIjoiW10iLCJ4LXJpZ2h0IjoiW10iLCJleHAiOjE2NzkzNjcwNTEsImlzcyI6IlMtRVJQIiwiYXVkIjoiUy1FUlAifQ.UeAI9WpxJORCvgX2d53AlD6WHZLspK0IX5i6aGONjgM");
                HttpResponseMessage getData = await client.GetAsync("timezones/" + code);

                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    var resultMasterFormated = JsonConvert.DeserializeObject<TimezoneMasterResposeModelPage>(result);
                    var res = resultMasterFormated?.Data;
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



