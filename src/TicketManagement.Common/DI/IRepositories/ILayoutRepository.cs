using System.Linq;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface ILayoutRepository : IRepository<Layout>
    {
        /// <summary>
        /// Method for populate data by id.
        /// </summary>
        /// <param name="venueId">venueId.</param>
        /// <returns>IQueryable&lt;<see cref="Layout"/>&gt;.</returns>
        IQueryable<Layout> GetAllByVenueId(int venueId);
    }
}
