using BookingService.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Booking> Bookings => Set<Booking>();
    }
}
