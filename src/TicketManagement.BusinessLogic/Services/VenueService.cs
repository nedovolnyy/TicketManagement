﻿using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

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

        public override void Validate(Venue entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new ValidationException("The field 'Name' of Venue is not allowed to be empty!");
            }
            else if (string.IsNullOrEmpty(entity.Address))
            {
                throw new ValidationException("The field 'Address' of Venue is not allowed to be empty!");
            }
            else if (string.IsNullOrEmpty(entity.Description))
            {
                throw new ValidationException("The field 'Description' of Venue is not allowed to be empty!");
            }
            else
            {
                var venueId = _venueRepository.GetIdFirstByName(entity.Name);
                if (venueId != default)
                {
                    throw new ValidationException("The Venue name is not unique!");
                }
            }
        }
    }
}
