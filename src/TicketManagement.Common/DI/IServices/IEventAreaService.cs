using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IEventAreaService : IService<EventArea>
    {
        Task ValidateAsync(EventArea entity);

        /// <summary>
        /// Count all EventArea by EventId.
        /// </summary>
        /// <param name="eventId">eventId.</param>
        /// <returns>List&lt;<see cref="EventArea"/>&gt;.</returns>
        Task<IEnumerable<EventArea>> GetAllByEventIdAsync(int eventId);
    }
}
