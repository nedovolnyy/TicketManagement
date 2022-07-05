using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IEventSeatService : IService<IEventSeat>
    {
        Task ValidateAsync(IEventSeat entity);

        /// <summary>
        /// Method for change EventSeat.Status after purchase seat.
        /// </summary>
        /// <param name="eventSeatId">eventAreaId.</param>
        /// <returns><see cref="int"/>.</returns>
        Task<int> ChangeEventSeatStatusAsync(int eventSeatId);

        /// <summary>
        /// Count all EventSeat by EventAreaId.
        /// </summary>
        /// <param name="eventAreaId">eventAreaId.</param>
        /// <returns>List&lt;<see cref="IEventSeat"/>&gt;.</returns>
        Task<IEnumerable<IEventSeat>> GetAllByEventAreaIdAsync(int eventAreaId);
    }
}
