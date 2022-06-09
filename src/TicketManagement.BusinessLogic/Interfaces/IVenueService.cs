using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IVenueService : IService<Venue>
    {
        void Validate(Venue entity);
    }
}
