using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.EF
{
    public partial class DatabaseContext : DbContext, IDatabaseContext
    {
        private readonly string _connectionString;
        private DbConnection _connection;

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

        public DbConnection Connection
        {
            get
            {
                _connection ??= new DatabaseContext(_connectionString).Database.GetDbConnection();
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                return _connection;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString).UseLoggerFactory(
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
