using TicketManagement.Common.Entities;

namespace TicketManagement.DI
{
    public interface IEventSeatService : IService<EventSeat>
    {
        Task Validate(EventSeat entity);
    }
}
