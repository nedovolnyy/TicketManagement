using System;
using System.Threading.Tasks;
using TicketManagement.Common.DI;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.Services
{
    internal class EventService : BaseService<IEvent>, IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
            : base(eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public virtual async Task<int> InsertAsync(IEvent evnt, decimal price)
        {
            await ValidateAsync(evnt);
            return await _eventRepository.InsertAsync(evnt, price);
        }

        public virtual async Task<int> GetSeatsAvailableCountAsync(int id)
            => await _eventRepository.GetSeatsAvailableCountAsync(id);
        public virtual async Task<int> GetSeatsCountAsync(int layoutId)
            => await _eventRepository.GetSeatsCountAsync(layoutId);

        private async Task EventValidate(IEvent entity)
        {
            if ((entity.EventTime.Ticks - DateTimeOffset.Now.Ticks) < 0)
            {
                throw new ValidationException("Event can't be created in the past!");
            }

            if (entity.EventTime > entity.EventEndTime)
            {
                throw new ValidationException("EventEndTime cannot be later than EventTime!");
            }

            var evntArray = await _eventRepository.GetAllByLayoutId(entity.LayoutId).ToListAsyncSafe();
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

            if (await GetSeatsCountAsync(entity.LayoutId) == default)
            {
                throw new ValidationException("Create event is not possible! Haven't seats in Area!");
            }
        }

        public override async Task ValidateAsync(IEvent entity)
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
            else if (string.IsNullOrEmpty(entity.EventLogoImage))
            {
                throw new ValidationException("The field 'EventLogoImage' of Event is not allowed to be empty!");
            }
            else
            {
                await EventValidate(entity);
            }
        }
    }
}
