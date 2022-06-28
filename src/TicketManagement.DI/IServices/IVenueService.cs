using TicketManagement.Common.Entities;

namespace TicketManagement.DI
{
    public interface IVenueService : IService<Venue>
    {
        Task Validate(Venue entity);
    }
}
