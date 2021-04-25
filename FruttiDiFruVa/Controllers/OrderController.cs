using FruttiDiFruVa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            OrderViewModel orderModel = new OrderViewModel();

            orderModel.ArticleItems = GetArticleItems(20);
            var recipients = GetAllRecipients();

            orderModel.Recipients = new SelectList(recipients, "Id", "Name");

            return View(orderModel);
        }

        public IActionResult Submit(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var selectedArticles = model.Order.Split(',') as IEnumerable<string>;

                Order order = new Order();
                order.Articles = new List<ArticleItem>();

                foreach (var articleAmountItem in selectedArticles)
                {
                    order.Articles.Add(new ArticleItem
                    {
                        Id = new Guid(articleAmountItem.Split(':')[0]),
                        Amount = Convert.ToInt32(articleAmountItem.Split(':')[1])
                    });
                }

                order.DeliveryDate = model.DeliveryDate;
                order.RecipientId = new Guid(model.RecipientId);

                //TODO Order in DB speichern


                return View("OrdersList");
            }

            OrderViewModel orderModel = new OrderViewModel();

            orderModel.ArticleItems = GetArticleItems(20);
            var recipients = GetAllRecipients();
            orderModel.Recipients = new SelectList(recipients, "Id", "Name");

            return View("~/Views/Order/Index.cshtml", orderModel);
        }

        private IEnumerable<ArticleItem> GetArticleItems(int take)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/api/articles");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<IEnumerable<ArticleItem>>(result).Take(take);
            }
        }

        private IEnumerable<Recipient> GetAllRecipients()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/api/recipients");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<IEnumerable<Recipient>>(result);
            }
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
