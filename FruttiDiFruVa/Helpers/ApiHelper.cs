using FruttiDiFruVa.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace FruttiDiFruVa.Helpers
{
    public static class ApiHelper
    {
        public static List<Order> GetAndSetApiValues()
        {
            List<Order> orders;

            using (var dbContext = new OrderDb())
            {
                orders = dbContext.Orders.Include(x => x.Articles).ToList();
            }

            var recipients = ApiHelper.GetAllRecipients();
            var articles = ApiHelper.GetArticleItems(20);

            foreach (var order in orders)
            {
                order.RecipientName = recipients.Where(x => x.Id.Equals(order.RecipientId)).Single().Name;

                foreach (var item in order.Articles)
                {
                    item.ArticleName = articles.Where(x => x.ArticleId.Equals(item.ArticleId)).First().ArticleName;
                }
            }

            return orders;
        }

        public static IEnumerable<ArticleItem> GetArticleItems(int take)
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

        public static IEnumerable<Recipient> GetAllRecipients()
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
    }
}
