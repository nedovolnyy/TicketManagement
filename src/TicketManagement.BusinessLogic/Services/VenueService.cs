using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
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

        public void Insert(Venue dto) =>
            _venueRepository.Insert(dto);

        public void Update(Venue dto) =>
            _venueRepository.Update(dto);

        public void Delete(int id) =>
            _venueRepository.Delete(id);

        public Venue GetById(int id) =>
            _venueRepository.GetById(id);

        public IEnumerable<Venue> GetAll() =>
            _venueRepository.GetAll();
    }
}
