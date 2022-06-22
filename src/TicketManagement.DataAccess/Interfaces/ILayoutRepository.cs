using System.Collections.Generic;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface ILayoutRepository : IRepository<Layout>
    {
        /// <summary>
        /// Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>List&lt;<see cref="Layout"/>&gt;.</returns>
        IEnumerable<Layout> GetAllByVenueId(int id);
    }
}
