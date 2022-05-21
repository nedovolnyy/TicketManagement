using TicketManagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.BusinessLogic.Assembler
{
    public class LayoutAssembler
    {
        public LayoutDto WriteDto(Layout layout)
        {
            var layoutDto = new LayoutDto
            {
                Id = layout.Id,
                VenueId = layout.VenueId,
                Description = layout.Description,
            };
            return layoutDto;
        }
    }
}
