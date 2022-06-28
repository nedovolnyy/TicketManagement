using TicketManagement.Common.Entities;

namespace TicketManagement.DI
{
    public interface IEventAreaService : IService<EventArea>
    {
        Task Validate(EventArea entity);
    }
}
