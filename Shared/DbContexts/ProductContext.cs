using Microsoft.EntityFrameworkCore;
using Shared.Bases;
using Shared.Entities;

namespace Shared.DbContexts
{
    public class ProductContext : DbContextBase
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasKey(x => new { x.ProductId, x.CategoryId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
