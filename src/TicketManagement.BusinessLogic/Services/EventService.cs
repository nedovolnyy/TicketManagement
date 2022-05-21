using TicketManagement.BusinessLogic.Assembler;
using TicketManagement.BusinessLogic.DTO;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.DataAccess.Facades;

namespace TicketManagement.BusinessLogic.Services
{
    public class EventService : IService<EventDto>
    {
        private readonly EventFacade _eventFacade;
        private readonly EventAssembler _eventAssembler;

        public EventService(EventFacade eventFacade, EventAssembler eventAssembler)
        {
            _eventFacade = eventFacade;
            _eventAssembler = eventAssembler;
        }

        public EventDto Get(int id)
        {
            var evnt = _eventFacade.GetById(id);
            return _eventAssembler.WriteDto(evnt);
        }
    }
}
