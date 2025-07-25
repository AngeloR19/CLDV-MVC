using Microsoft.EntityFrameworkCore;

namespace CLDVPOE25.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<EventItem> EventItem { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Venue> Venue { get; set; }
        public DbSet<EventType> EventType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // For DateTime column, if you want it to store only the date (no time)
            modelBuilder.Entity<EventItem>()
                .Property(e => e.DateOfEvent)
                .HasColumnType("date");  // This ensures only date is stored, not time
        }
    }
}
