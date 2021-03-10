using Microsoft.EntityFrameworkCore;
using RealEstateMVC.Models;

namespace RealEstateMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<House> House { get; set; }
        public DbSet<Image> Image { get; set; }
    }
}
