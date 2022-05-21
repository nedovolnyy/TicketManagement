using TicketManagement.BusinessLogic.Assembler;
using TicketManagement.BusinessLogic.DTO;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.DataAccess.Facades;

namespace TicketManagement.BusinessLogic.Services
{
    public class EventSeatService : IService<EventSeatDto>
    {
        private readonly EventSeatFacade _eventSeatFacade;
        private readonly EventSeatAssembler _eventSeatAssembler;

        public EventSeatService(EventSeatFacade eventSeatFacade, EventSeatAssembler eventSeatAssembler)
        {
            _eventSeatFacade = eventSeatFacade;
            _eventSeatAssembler = eventSeatAssembler;
        }

        public EventSeatDto Get(int id)
        {
            var eventSeatDto = _eventSeatFacade.GetById(id);
            return _eventSeatAssembler.WriteDto(eventSeatDto);
        }
    }
}
