using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface ISeatRepository : IRepository<ISeat>
    {
        /// <summary>
        /// Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>List&lt;<see cref="ISeat"/>&gt;.</returns>
        Task<IEnumerable<ISeat>> GetAllByAreaId(int id);
    }
}
