using System.Collections.Generic;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface ILayoutRepository : IRepository<Layout>
    {
        IEnumerable<Layout> GetAllByVenueId(int? id);
    }
}
