using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderService.Contracts;
using OrderService.Models;

namespace OrderService.Data
{
    public class OrderDbContext : DbContext, IOrderDbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(configuration.GetConnectionString("EducationalPlatformConnection"));
        }
        */
        // Seeding the data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = Guid.NewGuid(), ProductName = "Product A", Quantity = 10 },
                new Order { Id = Guid.NewGuid(), ProductName = "Product B", Quantity = 20 },
                new Order { Id = Guid.NewGuid(), ProductName = "Product C", Quantity = 30 }
            );
            base.OnModelCreating(modelBuilder);
        }
        public Task SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}