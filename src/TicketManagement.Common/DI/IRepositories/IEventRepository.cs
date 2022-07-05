using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IEventRepository : IRepository<IEvent>
    {
        /// <summary>
        /// Method for populate data by layoutId.
        /// </summary>
        /// <param name="layoutId">layoutId.</param>
        /// <returns>IQueryable&lt;<see cref="IEvent"/>&gt;.</returns>
        IQueryable<IEvent> GetAllByLayoutId(int layoutId);

        /// <summary>
        /// Count available seats.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="int"/>.</returns>
        Task<int> GetSeatsAvailableCountAsync(int id);

        /// <summary>
        /// Method for validation data by seats in Area.
        /// </summary>
        /// <param name="layoutId">layoutId.</param>
        /// <returns><see cref="int"/>.</returns>
        Task<int> GetSeatsCountAsync(int layoutId);
    }
}
