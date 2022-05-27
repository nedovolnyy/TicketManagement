using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class EventAreaService : IService<EventArea>
    {
        private readonly EventAreaRepository _eventAreaRepository;

        public EventAreaService()
        {
            _eventAreaRepository = new EventAreaRepository();
        }

        public void Insert(EventArea dto) =>
            _eventAreaRepository.Insert(dto);

        public void Update(EventArea dto) =>
            _eventAreaRepository.Update(dto);

        public void Delete(int id) =>
            _eventAreaRepository.Delete(id);

        public EventArea GetById(int id) =>
            _eventAreaRepository.GetById(id);

        public IEnumerable<EventArea> GetAll() =>
            _eventAreaRepository.GetAll();
    }
}