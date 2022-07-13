using System.Linq;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface ISeatRepository : IRepository<Seat>
    {
        /// <summary>
        /// Method for populate data by id.
        /// </summary>
        /// <param name="areaId">areaId.</param>
        /// <returns>IQueryable&lt;<see cref="Seat"/>&gt;.</returns>
        IQueryable<Seat> GetAllByAreaId(int areaId);
    }
}
