using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IEventService : IService<IEvent>
    {
        Task ValidateAsync(IEvent entity);

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
