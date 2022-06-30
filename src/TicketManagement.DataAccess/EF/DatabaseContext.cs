using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.IdentityEntities;

namespace TicketManagement.DataAccess.EF
{
    public partial class DatabaseContext : IdentityDbContext, IDatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
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
    }
}
