using TicketManagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.BusinessLogic.Assembler
{
    public class EventSeatAssembler
    {
        public EventSeatDto WriteDto(EventSeat eventSeat)
        {
            var eventSeatDto = new EventSeatDto
            {
                Id = eventSeat.Id,
                Number = eventSeat.Number,
                Row = eventSeat.Row,
                State = eventSeat.State,
            };
            return eventSeatDto;
        }
    }
}
