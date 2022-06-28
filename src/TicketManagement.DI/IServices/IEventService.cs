using TicketManagement.Common.Entities;

namespace TicketManagement.DI
{
    public interface IEventService : IService<Event>
    {
        Task Validate(Event entity);
        Task<int> GetSeatsAvailableCount(int id);
        Task<int> GetSeatsCount(int layoutId);
    }
}
