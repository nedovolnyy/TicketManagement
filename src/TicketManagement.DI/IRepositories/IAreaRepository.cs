using TicketManagement.Common.Entities;

namespace TicketManagement.DI
{
    public interface IAreaRepository : IRepository<Area>
    {
        /// <summary>
        /// Base Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>List&lt;<see cref="Area"/>&gt;.</returns>
        Task<IEnumerable<Area>> GetAllByLayoutId(int id);
    }
}
