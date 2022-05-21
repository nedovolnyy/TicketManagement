using TicketManagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.BusinessLogic.Assembler
{
    public class EventAssembler
    {
        public EventDto WriteDto(Event evnt)
        {
            var evntDto = new EventDto
            {
                Id = evnt.Id,
                Description = evnt.Description,
                LayoutId = evnt.LayoutId,
            };
            return evntDto;
        }
    }
}
