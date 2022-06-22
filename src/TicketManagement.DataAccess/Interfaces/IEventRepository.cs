using System.Collections.Generic;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        /// <summary>
        /// Method for populate data by layoutId.
        /// </summary>
        /// <param name="layoutId">layoutId.</param>
        /// <returns>List&lt;<see cref="Event"/>&gt;.</returns>
        IEnumerable<Event> GetAllByLayoutId(int layoutId);

        /// <summary>
        /// Count empty seats.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="int"/>.</returns>
        int GetCountEmptySeats(int id);

        /// <summary>
        /// Method for validation data by seats in Area.
        /// </summary>
        /// <param name="layoutId">layoutId.</param>
        /// <returns><see cref="int"/>.</returns>
        int GetCountSeats(int layoutId);
    }
}
