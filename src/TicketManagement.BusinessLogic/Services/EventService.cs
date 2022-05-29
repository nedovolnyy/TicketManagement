using System;
using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.BusinessLogic.Validation;
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

        public void Insert(Event entity)
        {
            if ((Convert.ToDateTime(entity.Description).Ticks - DateTime.Now.Ticks) < 0)
            {
                throw new ValidationException("Event can't be created in the past!", "");
            }

            IEnumerable<Event> evntArray = _eventRepository.GetAllByLayoutId(entity.LayoutId);
            foreach (Event evnt in evntArray)
            {
                if ((entity.LayoutId == evnt.LayoutId) && (entity.Description == evnt.Description))
                {
                    throw new ValidationException("Layout name should be unique in venue!", "");
                }
            }

            _eventRepository.Insert(entity);
        }

        public void Update(Event entity) =>
            _eventRepository.Update(entity);

        public void Delete(int id) =>
            _eventRepository.Delete(id);
        public void Delete(Event entity) =>
            _eventRepository.Delete(entity);

        public Event GetById(int id) =>
            _eventRepository.GetById(id);

        public IEnumerable<Event> GetAll() =>
            _eventRepository.GetAll();
    }
}
