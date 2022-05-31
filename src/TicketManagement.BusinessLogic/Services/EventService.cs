using System;
using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class EventService : BaseService<Event>, IService<Event>
    {
        private readonly IEventRepository _eventRepository;
        public EventService()
            : base()
        {
            EntityRepository = new EventRepository();
            _eventRepository = (IEventRepository)EntityRepository;
        }

        protected override IRepository<Event> EntityRepository { get; }

        protected override void Validation(Event entity)
        {
            if ((entity.EventTime.Ticks - DateTime.Now.Ticks) < 0)
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
