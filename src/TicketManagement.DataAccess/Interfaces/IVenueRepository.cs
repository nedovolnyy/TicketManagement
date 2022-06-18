using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IVenueRepository : IRepository<Venue>
    {
        int GetIdFirstByName(string name);
    }
}
