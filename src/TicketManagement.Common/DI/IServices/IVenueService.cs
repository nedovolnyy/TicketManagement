using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IVenueService : IService<IVenue>
    {
        Task Validate(IVenue entity);
    }
}
