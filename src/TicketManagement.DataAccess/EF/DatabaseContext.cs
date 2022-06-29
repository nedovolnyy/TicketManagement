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
        public virtual DbSet<Area> Areas { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<EventArea> EventAreas { get; set; } = null!;
        public virtual DbSet<EventSeat> EventSeats { get; set; } = null!;
        public virtual DbSet<Layout> Layouts { get; set; } = null!;
        public virtual DbSet<Seat> Seats { get; set; } = null!;
        public virtual DbSet<Venue> Venues { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
#pragma warning disable S125 // Sections of code should not be commented out
#pragma warning disable SA1005 // Single line comments should begin with single space
#pragma warning disable SA1515 // Single-line comment should be preceded by blank line
            {
                optionsBuilder.UseSqlServer(_connectionString);
                //.UseLoggerFactory(
                //    LoggerFactory.Create(
                //        b => b
                //            .AddProvider(new SqlLoggerProvider())
                //            .AddFilter(level => level >= LogLevel.Information)))
                //.EnableSensitiveDataLogging()
                //.EnableDetailedErrors();
            }
#pragma warning restore SA1515 // Single-line comment should be preceded by blank line
#pragma warning restore SA1005 // Single line comments should begin with single space
#pragma warning restore S125 // Sections of code should not be commented out
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
