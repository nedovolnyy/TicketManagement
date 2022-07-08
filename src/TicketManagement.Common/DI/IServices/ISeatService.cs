using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface ISeatService : IService<Seat>
    {
        Task ValidateAsync(Seat entity);

        /// <summary>
        /// Method for populate data by id.
        /// </summary>
        /// <param name="areaId">areaId.</param>
        /// <returns>IEnumerable&lt;<see cref="Seat"/>&gt;.</returns>
        Task<IEnumerable<Seat>> GetAllByAreaIdAsync(int areaId);
    }
}
