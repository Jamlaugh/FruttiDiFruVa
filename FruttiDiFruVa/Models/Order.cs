using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruttiDiFruVa.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public Guid RecipientId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public ICollection<ArticleItem> Articles { get; set; }
        
        [NotMapped]
        public string RecipientName { get; set; }
    }
}
