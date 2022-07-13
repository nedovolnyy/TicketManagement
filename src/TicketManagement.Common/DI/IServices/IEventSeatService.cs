using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IEventSeatService : IService<EventSeat>
    {
        Task ValidateAsync(EventSeat entity);

        /// <summary>
        /// Count all EventSeat by EventAreaId.
        /// </summary>
        /// <param name="eventAreaId">eventAreaId.</param>
        /// <returns>List&lt;<see cref="EventSeat"/>&gt;.</returns>
        Task<IEnumerable<EventSeat>> GetAllByEventAreaIdAsync(int eventAreaId);

        /// <summary>
        /// Method for change EventSeat.Status after purchase seat.
        /// </summary>
        /// <param name="eventSeatId">eventAreaId.</param>
        Task ChangeEventSeatStatusAsync(int eventSeatId);

        /// <summary>
        /// Method for change EventSeat.Status after purchase seat.
        /// </summary>
        /// <param name="eventSeatId">eventAreaId.</param>
        /// <param name="state">state.</param>
        Task ChangeEventSeatStatusAsync(int eventSeatId, State state = State.Available);
    }
}
