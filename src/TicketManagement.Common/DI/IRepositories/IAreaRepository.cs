using System.Linq;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IAreaRepository : IRepository<Area>
    {
        /// <summary>
        /// Base Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>IQueryable&lt;<see cref="Area"/>&gt;.</returns>
        IQueryable<Area> GetAllByLayoutId(int id);
    }
}
