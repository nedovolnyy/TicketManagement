using System.Threading.Tasks;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DI;

namespace TicketManagement.BusinessLogic.Services
{
    internal class EventAreaService : BaseService<EventArea>, IEventAreaService
    {
        public EventAreaService(IEventAreaRepository eventAreaRepository)
            : base(eventAreaRepository)
        {
        }

        public override async Task Validate(EventArea entity)
        {
            if (entity.EventId == default)
            {
                throw new ValidationException("The field 'LayoutId' of EventArea is not allowed to be null!");
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
