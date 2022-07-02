using System.Linq;

namespace TicketManagement.Common.DI
{
    public interface ILayoutRepository : IRepository<ILayout>
    {
        /// <summary>
        /// Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>IQueryable&lt;<see cref="ILayout"/>&gt;.</returns>
        IQueryable<ILayout> GetAllByVenueId(int id);
    }
}
