using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IEventSeatRepository : IRepository<IEventSeat>
    {
        /// <summary>
        /// Base Method for populate data by id.
        /// </summary>
        /// <param name="eventAreaId">eventAreaId.</param>
        /// <returns>IQueryable&lt;<see cref="IEventSeat"/>&gt;.</returns>
        IQueryable<IEventSeat> GetAllByEventAreaId(int eventAreaId);
    }
}
