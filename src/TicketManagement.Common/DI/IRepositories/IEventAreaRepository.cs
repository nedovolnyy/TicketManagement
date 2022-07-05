using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IEventAreaRepository : IRepository<IEventArea>
    {
        /// <summary>
        /// Base Method for populate data by id.
        /// </summary>
        /// <param name="eventId">eventId.</param>
        /// <returns>IQueryable&lt;<see cref="IEventArea"/>&gt;.</returns>
        IQueryable<IEventArea> GetAllByEventId(int eventId);
    }
}
