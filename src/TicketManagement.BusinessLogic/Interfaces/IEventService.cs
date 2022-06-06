using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IEventService : IService<Event>
    {
        void Validate(Event entity);
    }
}
