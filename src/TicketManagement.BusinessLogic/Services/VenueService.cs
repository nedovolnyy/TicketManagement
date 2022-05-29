using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.BusinessLogic.Validation;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class VenueService : IService<Venue>
    {
        private readonly VenueRepository _venueRepository;

        public VenueService()
        {
            _venueRepository = new VenueRepository();
        }

        public void Insert(Venue entity)
        {
            Venue venue = _venueRepository.GetFirstByName(entity.Description);
            if (!venue.IsEmpty())
            {
                throw new ValidationException("The Venue description has not unique!", "");
            }

            _venueRepository.Insert(entity);
        }

        public void Update(Venue entity) =>
            _venueRepository.Update(entity);
        public void Delete(int id) =>
            _venueRepository.Delete(id);
        public void Delete(Venue entity) =>
            _venueRepository.Delete(entity);
        public Venue GetById(int id) =>
            _venueRepository.GetById(id);
        public IEnumerable<Venue> GetAll() =>
            _venueRepository.GetAll();
    }
}
