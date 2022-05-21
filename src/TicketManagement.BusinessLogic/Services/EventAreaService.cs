using TicketManagement.BusinessLogic.Assembler;
using TicketManagement.BusinessLogic.DTO;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.DataAccess.Facades;

namespace TicketManagement.BusinessLogic.Services
{
    public class EventAreaService : IService<EventAreaDto>
    {
        private readonly EventAreaFacade _eventEventAreaFacade;
        private readonly EventAreaAssembler _eventEventAreaAssembler;

        public EventAreaService(EventAreaFacade eventEventAreaFacade, EventAreaAssembler eventEventAreaAssembler)
        {
            _eventEventAreaFacade = eventEventAreaFacade;
            _eventEventAreaAssembler = eventEventAreaAssembler;
        }

        public EventAreaDto Get(int id)
        {
            var eventEventArea = _eventEventAreaFacade.GetById(id);
            return _eventEventAreaAssembler.WriteDto(eventEventArea);
        }
    }
}
