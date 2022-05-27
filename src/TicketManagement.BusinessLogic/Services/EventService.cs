using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class EventService : IService<Event>
    {
        private readonly EventRepository _eventRepository;

        public EventService()
        {
            _eventRepository = new EventRepository();
        }

        public void Insert(Event dto) =>
            _eventRepository.Insert(dto);

        public void Update(Event dto) =>
            _eventRepository.Update(dto);

        public void Delete(int id) =>
            _eventRepository.Delete(id);

        public Event GetById(int id) =>
            _eventRepository.GetById(id);

        public IEnumerable<Event> GetAll() =>
            _eventRepository.GetAll();
    }
}
