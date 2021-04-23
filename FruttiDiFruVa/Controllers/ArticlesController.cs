using FruttiDiFruVa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FruttiDiFruVa.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(ILogger<ArticlesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/api/articles");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            Articles articlesModel = new Articles();            

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                articlesModel.ArticleItems = JsonConvert.DeserializeObject<IEnumerable<ArticleItem>>(result).Take(20);
            }

            return View(articlesModel);
        }

        public IActionResult Details(string Id)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format("http://localhost:8080/api/articles/{0}", Id));
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            ArticleItem articleItem;

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                articleItem = JsonConvert.DeserializeObject<ArticleItem>(result);
            }

            return View(articleItem);
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
