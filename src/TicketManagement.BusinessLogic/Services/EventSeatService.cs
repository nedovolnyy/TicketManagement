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

        public void Insert(EventSeat dto) =>
            _eventSeatRepository.Insert(dto);

        public void Update(EventSeat dto) =>
            _eventSeatRepository.Update(dto);

        public void Delete(int id) =>
            _eventSeatRepository.Delete(id);

        public EventSeat GetById(int id) =>
            _eventSeatRepository.GetById(id);

        public IEnumerable<EventSeat> GetAll() =>
            _eventSeatRepository.GetAll();
    }
}
