using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.EF
{
    public partial class DatabaseContext : IdentityDbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

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
