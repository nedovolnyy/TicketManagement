using System.Linq;

namespace TicketManagement.Common.DI
{
    public interface ISeatRepository : IRepository<ISeat>
    {
        /// <summary>
        /// Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>IQueryable&lt;<see cref="ISeat"/>&gt;.</returns>
        IQueryable<ISeat> GetAllByAreaId(int id);
    }
}
