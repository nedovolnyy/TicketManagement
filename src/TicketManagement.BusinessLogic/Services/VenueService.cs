using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    internal class VenueService : BaseService<Venue>
    {
        private readonly IVenueRepository _venueRepository;

        internal VenueService()
            : base(new VenueRepository())
        {
            _venueRepository = new VenueRepository();
        }

        public VenueService(IVenueRepository venueRepository)
            : base(venueRepository)
        {
            _venueRepository = venueRepository;
        }

        protected override void Validate(Venue entity)
        {
            var venue = _venueRepository.GetFirstByName(entity.Name);
            if (venue.Name != null)
            {
                throw new ValidationException("The Venue name has not unique!", "");
            }
        }
    }
}
