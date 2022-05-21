using TicketManagement.BusinessLogic.DTO;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IAEventSeatService
    {
        EventSeatDto GetEventSeat(int id);
    }
}
