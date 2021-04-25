using FruttiDiFruVa.Helpers;
using FruttiDiFruVa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace FruttiDiFruVa.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(ILogger<ArticlesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string Id)
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
    }
}
