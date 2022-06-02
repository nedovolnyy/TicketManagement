using System;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    internal class EventService : BaseService<Event>
    {
        private readonly IEventRepository _eventRepository;
        internal EventService()
        {
            EntityRepository = new EventRepository();
            _eventRepository = (IEventRepository)EntityRepository;
        }

        protected override IRepository<Event> EntityRepository { get; set; }

        protected override void Validate(Event entity)
        {
            if ((entity.EventTime.Ticks - DateTimeOffset.Now.Ticks) < 0)
            {
                throw new ValidationException("Event can't be created in the past!", "");
            }

            var evntArray = _eventRepository.GetAllByLayoutId(entity.LayoutId);
            foreach (var evnt in evntArray)
            {
                if ((entity.LayoutId == evnt.LayoutId) && (entity.Description == evnt.Description))
                {
                    throw new ValidationException("Layout name should be unique in venue!", "");
                }

                if ((entity.LayoutId == evnt.LayoutId) && (entity.EventTime == evnt.EventTime))
                {
                    throw new ValidationException("Do not create event for the same layout in the same time!", "");
                }
            }
        }
    }
}
