using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruttiDiFruVa.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string Recipient { get; set; }
        public DateTime DeliveryDate { get; set; }

        public List<ArticleItem> Articles { get; set; }
    }
}
