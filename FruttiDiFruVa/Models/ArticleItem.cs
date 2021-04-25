using System;
using System.ComponentModel.DataAnnotations;

namespace FruttiDiFruVa.Models
{
    public class ArticleItem
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        public int Amount { get; set; }

        [Display(Name = "Product Name")]
        public string ArticleName { get; set; }

        [Display(Name = "Main Article Number")]
        public int MainArticleNumber { get; set; }
        
        [Display(Name = "Main Article Number")]
        public int DetailArticleNumber { get; set; }

        [Display(Name = "Package Size")]
        public string PackageSize { get; set; }

        [Display(Name = "Article Group Number")]
        public string ArticleGroupNumber { get; set; }

        [Display(Name = "Article Group Name")]
        public string ArticleGroupName { get; set; }

        [Display(Name = "Package Origin Country")]
        public string OriginCountry { get; set; }

        [Display(Name = "Trade Class")]
        public string TradeClass { get; set; }

        [Display(Name = "Colli")]
        public decimal Colli { get; set; }

        [Display(Name = "Caliber")]
        public string Caliber { get; set; }

        [Display(Name = "Variety")]
        public string Variety { get; set; }

        [Display(Name = "Own Brand")]
        public string OwnBrand { get; set; }

        [Display(Name = "Search Query")]
        public string SearchQuery { get; set; }

        [Display(Name = "Row Version")]
        public string RowVersion { get; set; }
    }
}
