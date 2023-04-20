using Microsoft.EntityFrameworkCore;
using Shared.Bases;
using Shared.Entities;

namespace Shared.DbContexts
{
    public class CategoryContext : DbContextBase
    {
        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
