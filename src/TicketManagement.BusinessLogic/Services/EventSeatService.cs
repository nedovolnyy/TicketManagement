using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class EventSeatService : IService<EventSeat>
    {
        private readonly EventSeatRepository _eventSeatRepository;

        public EventSeatService()
        {
            _eventSeatRepository = new EventSeatRepository();
        }

        public void Insert(EventSeat entity) =>
            _eventSeatRepository.Insert(entity);

        public void Update(EventSeat entity) =>
            _eventSeatRepository.Update(entity);

        public void Delete(int id) =>
            _eventSeatRepository.Delete(id);
        public void Delete(EventSeat entity) =>
            _eventSeatRepository.Delete(entity);

        public EventSeat GetById(int id) =>
            _eventSeatRepository.GetById(id);

        public IEnumerable<EventSeat> GetAll() =>
            _eventSeatRepository.GetAll();
    }
}
