using Microsoft.EntityFrameworkCore;

namespace FruttiDiFruVa.Models
{
    public class OrderDb : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ASSESSMENT\SQLEXPRESS;Database=FruVa_Assessment_Orders;User Id=assessment;Password=AssessmentFruVa2021#;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleItem>().HasKey(x => new
            {
                x.ArticleId,
                x.OrderId
            });
        }
    }
}
