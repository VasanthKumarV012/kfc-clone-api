using FullStackApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
