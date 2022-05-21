using TicketManagement.BusinessLogic.Assembler;
using TicketManagement.BusinessLogic.DTO;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.DataAccess.Facades;

namespace TicketManagement.BusinessLogic.Services
{
    public class SeatService : IService<SeatDto>
    {
        private readonly SeatFacade _seatFacade;
        private readonly SeatAssembler _seatAssembler;

        public SeatService(SeatFacade seatFacade, SeatAssembler seatAssembler)
        {
            _seatFacade = seatFacade;
            _seatAssembler = seatAssembler;
        }

        public SeatDto Get(int id)
        {
            var seatDto = _seatFacade.GetById(id);
            return _seatAssembler.WriteDto(seatDto);
        }
    }
}
