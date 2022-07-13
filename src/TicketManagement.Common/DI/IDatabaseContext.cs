using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IDatabaseContext
    {
        string ConnectionString { get; }
        DbContext Instance { get; }
        DbSet<Area> Areas { get; set; }
        DbSet<EventArea> EventAreas { get; set; }
        DbSet<EventSeat> EventSeats { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Layout> Layouts { get; set; }
        DbSet<Seat> Seats { get; set; }
        DbSet<Venue> Venues { get; set; }
    }
}