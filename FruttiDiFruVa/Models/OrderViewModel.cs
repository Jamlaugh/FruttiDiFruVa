using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FruttiDiFruVa.Models
{
    public class OrderViewModel
    {
        public IEnumerable<ArticleItem> ArticleItems { get; set; }
        public SelectList Recipients { get; set; }

        /// <summary>
        /// comma separated string, each value includes an articleId:amount
        /// Example: d520d679-515b-431b-9156-0011a873d420:2,b6b51b7e-65f0-4a5b-b6c7-001a0baa943e:3
        /// </summary>
        [Required(ErrorMessage = "No order placed!")]
        public string Order { get; set; }

        [Required(ErrorMessage = "No recipient selected!")]
        [Display(Name = "Recipient")]
        public string RecipientId { get; set; }

        [Required(ErrorMessage = "No delivery date entered!")]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }
    }
}
