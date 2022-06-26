using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
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
