using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IEventAreaService : IService<EventArea>
    {
        void Validate(EventArea entity);
    }
}
