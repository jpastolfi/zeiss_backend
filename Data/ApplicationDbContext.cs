using Microsoft.EntityFrameworkCore;
using Zeiss_Api.Models;

namespace Zeiss_Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}

        public DbSet<Product> Products => Set<Product>();
    }
}