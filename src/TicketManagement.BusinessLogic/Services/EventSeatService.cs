using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.Services
{
    internal class EventSeatService : BaseService<IEventSeat>, IEventSeatService
    {
        private readonly IEventSeatRepository _eventSeatRepository;
        public EventSeatService(IEventSeatRepository eventSeatRepository)
            : base(eventSeatRepository)
        {
            _eventSeatRepository = eventSeatRepository;
        }

        public async Task<int> ChangeEventSeatStatusAsync(int eventSeatId)
        {
            var eventSeat = await GetByIdAsync(eventSeatId);
            eventSeat.State = !eventSeat.State;
            await UpdateAsync(eventSeat);
            return (int)EntityState.Modified;
        }

        public virtual async Task<IEnumerable<IEventSeat>> GetAllByEventAreaIdAsync(int eventAreaId)
            => await _eventSeatRepository.GetAllByEventAreaId(eventAreaId).ToListAsyncSafe();

        public override async Task ValidateAsync(IEventSeat entity)
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
