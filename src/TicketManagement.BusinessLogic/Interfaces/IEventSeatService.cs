using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IEventSeatService : IService<EventSeat>
    {
        void Validate(EventSeat entity);
    }
}
