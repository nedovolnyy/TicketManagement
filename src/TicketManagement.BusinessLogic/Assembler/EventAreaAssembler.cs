using TicketManagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.BusinessLogic.Assembler
{
    public class EventAreaAssembler
    {
        public EventAreaDto WriteDto(EventArea eventArea)
        {
            var eventAreaDto = new EventAreaDto
            {
                Id = eventArea.Id,
                Description = eventArea.Description,
                CoordX = eventArea.CoordX,
                CoordY = eventArea.CoordY,
                Price = eventArea.Price,
            };
            return eventAreaDto;
        }
    }
}
