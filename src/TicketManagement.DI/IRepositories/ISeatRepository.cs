using TicketManagement.Common.Entities;

namespace TicketManagement.DI
{
    public interface ISeatRepository : IRepository<Seat>
    {
        /// <summary>
        /// Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>List&lt;<see cref="Seat"/>&gt;.</returns>
        Task<IEnumerable<Seat>> GetAllByAreaId(int id);
    }
}
