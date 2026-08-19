using Microsoft.EntityFrameworkCore;
using zeiss_api.Models;

namespace zeiss_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}

        public DbSet<Product> Products => Set<Product>();
    }
}