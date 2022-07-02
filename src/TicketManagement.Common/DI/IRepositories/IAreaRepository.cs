using System.Linq;

namespace TicketManagement.Common.DI
{
    public interface IAreaRepository : IRepository<IArea>
    {
        /// <summary>
        /// Base Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>IQueryable&lt;<see cref="IArea"/>&gt;.</returns>
        IQueryable<IArea> GetAllByLayoutId(int id);
    }
}
