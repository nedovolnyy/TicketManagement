using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IVenueService : IService<Venue>
    {
        Task ValidateAsync(Venue entity);
    }
}
