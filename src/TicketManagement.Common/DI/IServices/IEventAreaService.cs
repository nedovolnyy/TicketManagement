using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IEventAreaService : IService<IEventArea>
    {
        Task ValidateAsync(IEventArea entity);

        /// <summary>
        /// Count all EventArea by EventId.
        /// </summary>
        /// <param name="eventId">eventId.</param>
        /// <returns>List&lt;<see cref="IEventArea"/>&gt;.</returns>
        Task<IEnumerable<IEventArea>> GetAllByEventIdAsync(int eventId);
    }
}
