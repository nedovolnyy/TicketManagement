using System.Collections.Generic;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface ISeatRepository : IRepository<Seat>
    {
        IEnumerable<Seat> GetAllByAreaId(int id);
    }
}
