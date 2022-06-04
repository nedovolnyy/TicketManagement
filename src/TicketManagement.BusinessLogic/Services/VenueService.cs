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
        {
            EntityRepository = new VenueRepository();
            _venueRepository = (IVenueRepository)EntityRepository;
        }

        protected override IRepository<Venue> EntityRepository { get; set; }

        protected override void Validate(Venue entity)
        {
            var venue = _venueRepository.GetFirstByName(entity.Description);
            if (venue.Description != null)
            {
                throw new ValidationException("The Venue description has not unique!", "");
            }
        }
    }
}
