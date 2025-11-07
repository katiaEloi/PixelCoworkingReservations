using Microsoft.EntityFrameworkCore;
using SpacesService.Data;

namespace SpacesService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Space> Spaces => Set<Space>();
    }
    
}
