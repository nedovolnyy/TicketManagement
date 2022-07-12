using System.Linq;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IEventSeatRepository : IRepository<EventSeat>
    {
        /// <summary>
        /// Base Method for populate data by id.
        /// </summary>
        /// <param name="eventAreaId">eventAreaId.</param>
        /// <returns>IQueryable&lt;<see cref="EventSeat"/>&gt;.</returns>
        IQueryable<EventSeat> GetAllByEventAreaId(int eventAreaId);

        /// <summary>
        /// Method for change EventSeat.Status after purchase seat.
        /// </summary>
        /// <param name="eventSeatId">eventSeatId.</param>
        /// <param name="state">state.</param>
        Task ChangeEventSeatStatusAsync(int eventSeatId, State state = State.Available);
    }
}
