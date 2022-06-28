using TicketManagement.Common.Entities;

namespace TicketManagement.DI
{
    public interface ILayoutRepository : IRepository<Layout>
    {
        /// <summary>
        /// Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>List&lt;<see cref="Layout"/>&gt;.</returns>
        Task<IEnumerable<Layout>> GetAllByVenueId(int id);
    }
}
