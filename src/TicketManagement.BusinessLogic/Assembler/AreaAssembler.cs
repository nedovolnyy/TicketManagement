using TicketManagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.BusinessLogic.Assembler
{
    public class AreaAssembler
    {
        public AreaDto WriteDto(Area area)
        {
            var areaDto = new AreaDto
            {
                Id = area.Id,
                Description = area.Description,
                CoordX = area.CoordX,
                CoordY = area.CoordY,
            };
            return areaDto;
        }
    }
}
