using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IEventRepository : IRepository<IEvent>
    {
        /// <summary>
        /// Method for populate data by layoutId.
        /// </summary>
        /// <param name="layoutId">layoutId.</param>
        /// <returns>List&lt;<see cref="IEvent"/>&gt;.</returns>
        Task<IEnumerable<IEvent>> GetAllByLayoutId(int layoutId);

        /// <summary>
        /// Count empty seats.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="int"/>.</returns>
        Task<int> GetSeatsAvailableCount(int id);

        /// <summary>
        /// Method for validation data by seats in Area.
        /// </summary>
        /// <param name="layoutId">layoutId.</param>
        /// <returns><see cref="int"/>.</returns>
        Task<int> GetSeatsCount(int layoutId);
    }
}
