using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Identity;

namespace TicketManagement.DataAccess.EF
{
    public partial class DatabaseContext : IdentityDbContext<User, Role, string>, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            ConnectionString = Database.GetConnectionString();
        }

        public string ConnectionString { get; private set; }
        public DbContext Instance => this;
        public virtual DbSet<Area> Areas { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<EventArea> EventAreas { get; set; } = null!;
        public DbSet<EventSeat> EventSeats { get; set; } = null!;
        public DbSet<Layout> Layouts { get; set; } = null!;
        public DbSet<Seat> Seats { get; set; } = null!;
        public DbSet<Venue> Venues { get; set; } = null!;
    }
}
