using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.EF
{
    public partial class DatabaseContext : DbContext, IDatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbContext Instance => this;
        public DbSet<Area> Areas { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<EventArea> EventAreas { get; set; } = null!;
        public DbSet<EventSeat> EventSeats { get; set; } = null!;
        public DbSet<Layout> Layouts { get; set; } = null!;
        public DbSet<Seat> Seats { get; set; } = null!;
        public DbSet<Venue> Venues { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .UseLoggerFactory(
                        LoggerFactory.Create(
                            b => b
                                .AddProvider(new SqlLoggerProvider())
                                .AddFilter(level => level >= LogLevel.Information)))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Global turn off delete behaviour on foreign keys
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }
    }
}
