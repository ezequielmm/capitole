using Capitole.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Capitole.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
