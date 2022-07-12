using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.Services
{
    internal class EventSeatService : BaseService<EventSeat>, IEventSeatService
    {
        private readonly IEventSeatRepository _eventSeatRepository;
        public EventSeatService(IEventSeatRepository eventSeatRepository)
            : base(eventSeatRepository)
        {
            _eventSeatRepository = eventSeatRepository;
        }

        public async Task ChangeEventSeatStatusAsync(int eventSeatId, State state = State.Available)
            => await _eventSeatRepository.ChangeEventSeatStatusAsync(eventSeatId, state);

        public virtual async Task<IEnumerable<EventSeat>> GetAllByEventAreaIdAsync(int eventAreaId)
            => await _eventSeatRepository.GetAllByEventAreaId(eventAreaId).ToListAsyncSafe();

        public override async Task ValidateAsync(EventSeat entity)
        {
            if (entity.EventAreaId == default)
            {
                throw new ValidationException("The field 'EventAreaId' of EventSeat is not allowed to be null!");
            }

            if (entity.Row == default)
            {
                throw new ValidationException("The field 'Row' of EventSeat is not allowed to be null!");
            }

            if (entity.Number == default)
            {
                throw new ValidationException("The field 'Number' of EventSeat is not allowed to be null!");
            }

            await Task.Delay(100);
        }
    }
}
