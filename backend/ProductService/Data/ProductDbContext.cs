using Microsoft.EntityFrameworkCore;
using ProductService.Contracts;
using ProductService.Models;

namespace ProductService.Data
{
    public class ProductDbContext : DbContext, IProductDbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(configuration.GetConnectionString("EducationalPlatformConnection"));
        }
        */
        // Seeding the data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product X", Price = 9.99m, Stock = 100 },
                new Product { Id = 2, Name = "Product Y", Price = 19.99m, Stock = 50 },
                new Product { Id = 3, Name = "Product Z", Price = 29.99m, Stock = 25 }
            );
            base.OnModelCreating(modelBuilder);
        }
        public Task SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}