using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IDatabaseContext
    {
        DbContext Instance { get; }
        DbSet<Area> Areas { get; set; }
        DbSet<EventArea> EventAreas { get; set; }
        DbSet<EventSeat> EventSeats { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Layout> Layouts { get; set; }
        DbSet<Seat> Seats { get; set; }
        DbSet<Venue> Venues { get; set; }
        DbConnection Connection { get; }
    }
}