using System.Collections.Generic;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IAreaRepository : IRepository<Area>
    {
        /// <summary>
        /// Base Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>List&lt;<see cref="Area"/>&gt;.</returns>
        IEnumerable<Area> GetAllByLayoutId(int id);
    }
}
