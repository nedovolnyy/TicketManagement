using System;
using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.BusinessLogic.Validation;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class EventService : BaseService<Event>, IService<Event>
    {
        private readonly EventRepository _eventRepository;
        public EventService()
            : base()
        {
            EntityRepository = new EventRepository();
            _eventRepository = (EventRepository)EntityRepository;
        }

        protected override BaseRepository<Event> EntityRepository { get; }

        protected override void Validation(Event entity)
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
        }
    }
}
