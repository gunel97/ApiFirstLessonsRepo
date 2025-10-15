using _111025APItest.Models;
using Microsoft.EntityFrameworkCore;

namespace _111025APItest.DataContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        public DbSet<Student> Students { get; set; }
           public DbSet<Product> Products {  get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
