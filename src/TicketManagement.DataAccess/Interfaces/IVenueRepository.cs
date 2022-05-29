using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IVenueRepository : IRepository<Venue>
    {
        Venue GetFirstByName(string name);
    }
}
