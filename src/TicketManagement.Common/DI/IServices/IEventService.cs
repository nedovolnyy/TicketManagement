using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IEventService : IService<Event>
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

        Task ValidateAsync(Event entity);

        /// <summary>
        /// Method for populate data by layoutId.
        /// </summary>
        /// <param name="layoutId">layoutId.</param>
        /// <returns>IEnumerable&lt;<see cref="Event"/>&gt;.</returns>
        Task<IEnumerable<Event>> GetAllByLayoutIdAsync(int layoutId);

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
        /// Count all seats in Area.
        /// </summary>
        /// <param name="layoutId">layoutId.</param>
        /// <returns><see cref="int"/>.</returns>
        Task<int> GetSeatsCountAsync(int layoutId);
    }
}
