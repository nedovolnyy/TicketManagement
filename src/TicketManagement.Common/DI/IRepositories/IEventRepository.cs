using System.Linq;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IEventRepository : IRepository<Event>
    {
        /// <summary>
        /// Special method for create Event.
        /// </summary>
        /// <param name="evnt">Entity.</param>
        /// <param name="price">Price.</param>
        Task InsertAsync(Event evnt, decimal price);

        /// <summary>
        /// Special method for update Event.
        /// </summary>
        /// <param name="evnt">Entity.</param>
        /// <param name="price">Price.</param>
        Task UpdateAsync(Event evnt, decimal price);

        /// <summary>
        /// Method for populate data by layoutId.
        /// </summary>
        /// <param name="layoutId">layoutId.</param>
        /// <returns>IQueryable&lt;<see cref="Event"/>&gt;.</returns>
        IQueryable<Event> GetAllByLayoutId(int layoutId);

        /// <summary>
        /// Get price by Event.Id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="decimal"/>.</returns>
        Task<decimal> GetPriceByEventIdAsync(int id);

        /// <summary>
        /// All available seats or not.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="bool"/>.</returns>
        Task<bool> IsAllAvailableSeatsAsync(int id);

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
