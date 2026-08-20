using Microsoft.EntityFrameworkCore;
using zeiss_api.Models;

namespace zeiss_api.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Title = "Electronics" },
                new Category { Id = 2, Title = "Office Supplies" },
                new Category { Id = 3, Title = "Furniture" },
                new Category { Id = 4, Title = "Sports & Outdoors" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 100001, Name = "Wireless Mouse", Price = 24.99M, Stock = 150, CategoryId = 1 },
                new Product { Id = 100002, Name = "Wireless Keyboard", Price = 49.99M, Stock = 85, CategoryId = 1 },
                new Product { Id = 100003, Name = "Wireless Headphones", Price = 89.99M, Stock = 0, CategoryId = 1 },
                new Product { Id = 100004, Name = "USB-C Charging Cable", Price = 12.99M, Stock = 500, CategoryId = 1 },
                new Product { Id = 100005, Name = "Standing Desk Converter", Price = 129.99M, Stock = 12, CategoryId = 2 },
                new Product { Id = 100006, Name = "Desk Lamp", Price = 34.50M, Stock = 40, CategoryId = 2 },
                new Product { Id = 100007, Name = "Mechanical Pencil Set", Price = 8.99M, Stock = 300, CategoryId = 2 },
                new Product { Id = 100008, Name = "Standing Desk", Price = 349.00M, Stock = 8, CategoryId = 3 },
                new Product { Id = 100009, Name = "Ergonomic Office Chair", Price = 219.99M, Stock = 25, CategoryId = 3 },
                new Product { Id = 100010, Name = "Yoga Mat", Price = 19.99M, Stock = 60, CategoryId = 4 },
                new Product { Id = 100011, Name = "Water Bottle", Price = 9.99M, Stock = 1000, CategoryId = 4 }
            );
        }
    }
}