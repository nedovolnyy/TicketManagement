using TicketManagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.BusinessLogic.Assembler
{
    public class SeatAssembler
    {
        public SeatDto WriteDto(Seat seat)
        {
            var seatDto = new SeatDto
            {
                Id = seat.Id,
                AreaId = seat.AreaId,
                Row = seat.Row,
                Number = seat.Number,
            };
            return seatDto;
        }
    }
}
