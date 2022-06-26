using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
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
