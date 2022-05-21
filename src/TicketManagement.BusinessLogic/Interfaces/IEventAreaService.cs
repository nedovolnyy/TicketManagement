using TicketManagement.BusinessLogic.DTO;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IEventAreaService
    {
        EventAreaDto GetEventArea(int id);
    }
}
