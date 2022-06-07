using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    internal class VenueService : BaseService<Venue>, IVenueService
    {
        private readonly IVenueRepository _venueRepository;

        internal VenueService()
        {
            _venueRepository = new VenueRepository();
        }

        public VenueService(IVenueRepository venueRepository)
            : base(venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public override void Validate(Venue entity)
        {
            if ((entity.Name == "") | (entity.Address == "") | (entity.Description == ""))
            {
                throw new ValidationException("The field of Venue is not allowed to be null!", "");
            }

            var venue = _venueRepository.GetFirstByName(entity.Name);
            if (venue.Name != null)
            {
                throw new ValidationException("The Venue name has not unique!", "");
            }
        }
    }
}
