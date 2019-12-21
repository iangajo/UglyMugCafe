using Microsoft.EntityFrameworkCore;
using Ugly.Mug.Cafe.Domain.Entity;

namespace Ugly.Mug.Cafe.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => new { p.ProductId });
            modelBuilder.Entity<Order>().HasKey(p => new { Id = p.OrderId });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
