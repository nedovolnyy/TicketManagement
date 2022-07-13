using System.Linq;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IEventAreaRepository : IRepository<EventArea>
    {
        /// <summary>
        /// Base Method for populate data by id.
        /// </summary>
        /// <param name="eventId">eventId.</param>
        /// <returns>IQueryable&lt;<see cref="EventArea"/>&gt;.</returns>
        IQueryable<EventArea> GetAllByEventId(int eventId);
    }
}
