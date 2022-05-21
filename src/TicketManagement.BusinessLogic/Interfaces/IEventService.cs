using TicketManagement.BusinessLogic.DTO;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IEventService
    {
        EventDto GetEvent(int id);
    }
}
