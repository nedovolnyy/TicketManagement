using System.Collections.Generic;
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
        IEnumerable<Seat> GetAllByAreaId(int id);
    }
}
