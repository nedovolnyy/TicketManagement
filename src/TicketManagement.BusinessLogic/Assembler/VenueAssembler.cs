using TicketManagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.BusinessLogic.Assembler
{
    public class VenueAssembler
    {
        public VenueDto WriteDto(Venue venue)
        {
            var venueDto = new VenueDto
            {
                Id = venue.Id,
                Description = venue.Description,
                Address = venue.Address,
                Phone = venue.Phone,
            };
            return venueDto;
        }
    }
}
