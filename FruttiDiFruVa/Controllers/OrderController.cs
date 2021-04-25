using FruttiDiFruVa.Helpers;
using FruttiDiFruVa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

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
            var orders = ApiHelper.GetAndSetApiValues();

            return View(orders);
        }

        public IActionResult Edit(int id)
        {
            Order order;

            using (var dbContext = new OrderDb())
            {
                order = dbContext.Orders
                    .Include(x => x.Articles)
                    .Where(x => x.Id == id)
                    .First();
            }

            return View("~/Views/Order/Edit.cshtml", order);
        }

        public IActionResult SubmitEdit(Order model)
        {
            Order order;

            if (ModelState.IsValid)
            {
                using (var dbContext = new OrderDb())
                {
                    order = dbContext.Orders
                        .Include(x => x.Articles)
                        .Where(x => x.Id == model.Id)
                        .First();

                    order.DeliveryDate = model.DeliveryDate;

                    dbContext.SaveChanges();
                }

                var orders = ApiHelper.GetAndSetApiValues();

                return View("~/Views/Order/index.cshtml", orders);
            }

            return View("~/Views/Order/Edit.cshtml", model);
        }

        public IActionResult Delete(string id)
        {
            using (var dbContext = new OrderDb())
            {
                var orderToRemove = dbContext.Orders.Include(x => x.Articles)
                    .Where(x => x.Id.ToString().Equals(id))
                    .First();

                var articleItemsToRemove = orderToRemove.Articles;
                
                dbContext.Orders.Remove(orderToRemove);
                dbContext.SaveChanges();
            }

            var orders = ApiHelper.GetAndSetApiValues();

            return View("~/Views/Order/index.cshtml", orders);
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
