using Microsoft.EntityFrameworkCore;
using ProductTaskWebAPI.Server.Models;

namespace ProductTaskWebAPI.Server.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
