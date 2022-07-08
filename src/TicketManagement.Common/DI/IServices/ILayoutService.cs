using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface ILayoutService : IService<Layout>
    {
        Task ValidateAsync(Layout entity);

        /// <summary>
        /// Method for populate data by id.
        /// </summary>
        /// <param name="venueId">venueId.</param>
        /// <returns>IEnumerable&lt;<see cref="Layout"/>&gt;.</returns>
        Task<IEnumerable<Layout>> GetAllByVenueIdAsync(int venueId);
    }
}
