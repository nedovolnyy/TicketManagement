using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.BusinessLogic.Validation;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class VenueService : BaseService<Venue>, IService<Venue>
    {
        private readonly VenueRepository _venueRepository;
        public VenueService()
            : base()
        {
            EntityRepository = new VenueRepository();
            _venueRepository = (VenueRepository)EntityRepository;
        }

        protected override BaseRepository<Venue> EntityRepository { get; }

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
