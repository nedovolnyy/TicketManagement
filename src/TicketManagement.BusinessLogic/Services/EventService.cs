using System;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

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

        public int GetCountEmptySeats(int id)
            => _eventRepository.GetCountEmptySeats(id);
        public int GetCountSeats(int layoutId)
            => _eventRepository.GetCountSeats(layoutId);

        private void EventValidation(Event entity)
        {
            if ((entity.EventTime.Ticks - DateTimeOffset.Now.Ticks) < 0)
            {
                throw new ValidationException("Event can't be created in the past!");
            }

            if (entity.EventTime > entity.EventEndTime)
            {
                throw new ValidationException("EventEndTime cannot be later than EventTime!");
            }

            var evntArray = _eventRepository.GetAllByLayoutId(entity.LayoutId);
            foreach (var evnt in evntArray)
            {
                if (entity.LayoutId == evnt.LayoutId && entity.Name == evnt.Name)
                {
                    throw new ValidationException("Layout name should be unique in venue!");
                }

                if (entity.LayoutId == evnt.LayoutId && entity.EventTime == evnt.EventTime)
                {
                    throw new ValidationException("Do not create event for the same layout in the same time!");
                }
            }

            if (GetCountSeats(entity.LayoutId) == default)
            {
                throw new ValidationException("Create event is not possible! Haven't seats in Area!");
            }
        }

        public override void Validate(Event entity)
        {
            if (entity.LayoutId == default)
            {
                throw new ValidationException("The field 'LayoutId' of Event is not allowed to be null!");
            }
            else if (string.IsNullOrEmpty(entity.Name))
            {
                throw new ValidationException("The field 'Name' of Event is not allowed to be empty!");
            }
            else if (string.IsNullOrEmpty(entity.Description))
            {
                throw new ValidationException("The field 'Description' of Event is not allowed to be empty!");
            }
            else
            {
                EventValidation(entity);
            }
        }
    }
}
