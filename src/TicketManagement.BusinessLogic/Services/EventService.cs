using System;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    internal class EventService : BaseService<Event>, IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
            : base(eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public override void Validate(Event entity)
        {
            if ((entity.LayoutId == 0) | (entity.EventTime == DateTimeOffset.MinValue) | (entity.Name == "") | (entity.Description == ""))
            {
                throw new ValidationException("The field of Event is not allowed to be null!");
            }

            if ((entity.EventTime.Ticks - DateTimeOffset.Now.Ticks) < 0)
            {
                throw new ValidationException("Event can't be created in the past!");
            }

            var evntArray = _eventRepository.GetAllByLayoutId(entity.LayoutId);
            foreach (var evnt in evntArray)
            {
                if ((entity.LayoutId == evnt.LayoutId) && (entity.Description == evnt.Description))
                {
                    throw new ValidationException("Layout name should be unique in venue!");
                }

                if ((entity.LayoutId == evnt.LayoutId) && (entity.EventTime == evnt.EventTime))
                {
                    throw new ValidationException("Do not create event for the same layout in the same time!");
                }
            }
        }
    }
}
