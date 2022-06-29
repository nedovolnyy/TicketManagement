using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IAreaRepository : IRepository<IArea>
    {
        /// <summary>
        /// Base Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>List&lt;<see cref="IArea"/>&gt;.</returns>
        Task<IEnumerable<IArea>> GetAllByLayoutId(int id);
    }
}
