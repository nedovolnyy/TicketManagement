using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface ILayoutRepository : IRepository<ILayout>
    {
        /// <summary>
        /// Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>List&lt;<see cref="ILayout"/>&gt;.</returns>
        Task<IEnumerable<ILayout>> GetAllByVenueId(int id);
    }
}
