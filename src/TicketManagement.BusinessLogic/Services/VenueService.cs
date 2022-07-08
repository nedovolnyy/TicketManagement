using System.Threading.Tasks;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.Services
{
    internal class VenueService : BaseService<Venue>, IVenueService
    {
        private readonly IVenueRepository _venueRepository;
        public VenueService(IVenueRepository venueRepository)
            : base(venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public override async Task ValidateAsync(Venue entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new ValidationException("The field 'Name' of Venue is not allowed to be empty!");
            }

            if (string.IsNullOrEmpty(entity.Address))
            {
                throw new ValidationException("The field 'Address' of Venue is not allowed to be empty!");
            }

            if (string.IsNullOrEmpty(entity.Description))
            {
                throw new ValidationException("The field 'Description' of Venue is not allowed to be empty!");
            }

            var venueId = await _venueRepository.GetIdFirstByNameAsync(entity.Name);
            if (venueId != default)
            {
                throw new ValidationException("The Venue name is not unique!");
            }
        }
    }
}
