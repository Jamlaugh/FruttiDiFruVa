using FruttiDiFruVa.Helpers;
using FruttiDiFruVa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FruttiDiFruVa.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;

        public ShopController(ILogger<ShopController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            OrderViewModel orderModel = CreateOrderViewModel();

            return View(orderModel);
        }

        private OrderViewModel CreateOrderViewModel()
        {
            OrderViewModel orderModel = new OrderViewModel();

            orderModel.ArticleItems = ApiHelper.GetArticleItems(20);
            var recipients = ApiHelper.GetAllRecipients();

            orderModel.Recipients = new SelectList(recipients, "Id", "Name");
            return orderModel;
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
                        ArticleId = new Guid(articleAmountItem.Split(':')[0]),
                        Amount = Convert.ToInt32(articleAmountItem.Split(':')[1])
                    });
                }

                order.DeliveryDate = model.DeliveryDate;
                order.RecipientId = new Guid(model.RecipientId);

                using (var dbModel = new OrderDb())
                {
                    dbModel.Orders.Add(order);
                    dbModel.SaveChanges();
                }

                var orders = ApiHelper.GetAndSetApiValues();

                return View("~/Views/Order/index.cshtml", orders);
            }

            OrderViewModel orderModel = CreateOrderViewModel();

            return View("~/Views/Shop/index.cshtml", orderModel);
        }
    }
}
