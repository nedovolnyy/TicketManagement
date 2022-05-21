using TicketManagement.BusinessLogic.Assembler;
using TicketManagement.BusinessLogic.DTO;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.DataAccess.Facades;

namespace TicketManagement.BusinessLogic.Services
{
    public class VenueService : IService<VenueDto>
    {
        private readonly VenueFacade _venueFacade;
        private readonly VenueAssembler _venueAssembler;

        public VenueService(VenueFacade venueFacade, VenueAssembler venueAssembler)
        {
            _venueFacade = venueFacade;
            _venueAssembler = venueAssembler;
        }

        public VenueDto Get(int id)
        {
            var venueDto = _venueFacade.GetById(id);
            return _venueAssembler.WriteDto(venueDto);
        }
    }
}
