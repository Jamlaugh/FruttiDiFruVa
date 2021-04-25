using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruttiDiFruVa.Models
{
    public class OrderDb : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<ArticleItem> Articles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ASSESSMENT\SQLEXPRESS;Database=FruVa_Assessment_Orders;User Id=assessment;Password=AssessmentFruVa2021#;");
        }
    }
}
