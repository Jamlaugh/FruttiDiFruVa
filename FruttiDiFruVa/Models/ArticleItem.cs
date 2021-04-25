using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruttiDiFruVa.Models
{
    public class ArticleItem
    {
        public int OrderId { get; set; }

        [JsonProperty("Id")]
        [Display(Name = "Id")]
        public Guid ArticleId { get; set; }

        public int Amount { get; set; }

        [NotMapped]
        [Display(Name = "Product Name")]
        public string ArticleName { get; set; }

        [NotMapped]
        [Display(Name = "Main Article Number")]
        public int MainArticleNumber { get; set; }

        [NotMapped]
        [Display(Name = "Main Article Number")]
        public int DetailArticleNumber { get; set; }

        [NotMapped]
        [Display(Name = "Package Size")]
        public string PackageSize { get; set; }

        [NotMapped]
        [Display(Name = "Article Group Number")]
        public string ArticleGroupNumber { get; set; }

        [NotMapped]
        [Display(Name = "Article Group Name")]
        public string ArticleGroupName { get; set; }

        [NotMapped]
        [Display(Name = "Package Origin Country")]
        public string OriginCountry { get; set; }

        [NotMapped]
        [Display(Name = "Trade Class")]
        public string TradeClass { get; set; }

        [NotMapped]
        [Display(Name = "Colli")]
        public decimal Colli { get; set; }

        [NotMapped]
        [Display(Name = "Caliber")]
        public string Caliber { get; set; }

        [NotMapped]
        [Display(Name = "Variety")]
        public string Variety { get; set; }

        [NotMapped]
        [Display(Name = "Own Brand")]
        public string OwnBrand { get; set; }

        [NotMapped]
        [Display(Name = "Search Query")]
        public string SearchQuery { get; set; }

        [NotMapped]
        [Display(Name = "Row Version")]
        public string RowVersion { get; set; }
    }
}
