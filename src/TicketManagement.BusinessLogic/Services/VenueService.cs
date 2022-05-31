using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class VenueService : BaseService<Venue>, IService<Venue>
    {
        private readonly IVenueRepository _venueRepository;
        public VenueService()
            : base()
        {
            EntityRepository = new VenueRepository();
            _venueRepository = (IVenueRepository)EntityRepository;
        }

        protected override IRepository<Venue> EntityRepository { get; }

        protected override void Validation(Venue entity)
        {
            Venue venue = _venueRepository.GetFirstByName(entity.Description);
            if (!venue.IsEmpty())
            {
                throw new ValidationException("The Venue description has not unique!", "");
            }
        }
    }
}
