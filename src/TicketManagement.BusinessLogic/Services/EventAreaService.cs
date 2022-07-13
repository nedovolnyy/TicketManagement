using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.Services
{
    internal class EventAreaService : BaseService<EventArea>, IEventAreaService
    {
        private readonly IEventAreaRepository _eventAreaRepository;
        public EventAreaService(IEventAreaRepository eventAreaRepository)
            : base(eventAreaRepository)
        {
            _eventAreaRepository = eventAreaRepository;
        }

        public virtual async Task<IEnumerable<EventArea>> GetAllByEventIdAsync(int eventId)
            => await _eventAreaRepository.GetAllByEventId(eventId).ToListAsyncSafe();

        public override async Task ValidateAsync(EventArea entity)
        {
            if (entity.EventId == default)
            {
                throw new ValidationException("The field 'EventId' of EventArea is not allowed to be null!");
            }

            if (entity.CoordX == default)
            {
                throw new ValidationException("The field 'CoordX' of EventArea is not allowed to be null!");
            }

            if (entity.CoordY == default)
            {
                throw new ValidationException("The field 'CoordY' of EventArea is not allowed to be null!");
            }

            if (entity.Price == default)
            {
                throw new ValidationException("The field 'Price' of EventArea is not allowed to be null!");
            }

            if (string.IsNullOrEmpty(entity.Description))
            {
                throw new ValidationException("The field 'Description' of EventArea is not allowed to be empty!");
            }

            await Task.Delay(100);
        }
    }
}
