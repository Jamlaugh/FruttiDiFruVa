using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruttiDiFruVa.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Guid RecipientId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<ArticleItem> Articles { get; set; }
    }
}
