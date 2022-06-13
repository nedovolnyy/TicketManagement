using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.Services
{
    internal class EventAreaService : BaseService<EventArea>, IEventAreaService
    {
        public EventAreaService(IEventAreaRepository eventAreaRepository)
            : base(eventAreaRepository)
        {
        }

        public override void Validate(EventArea entity)
        {
            if (entity.EventId == default)
            {
                throw new ValidationException("The field 'LayoutId' of EventArea is not allowed to be null!");
            }
            else if (entity.CoordX == default)
            {
                throw new ValidationException("The field 'CoordX' of EventArea is not allowed to be null!");
            }
            else if (entity.CoordY == default)
            {
                throw new ValidationException("The field 'CoordY' of EventArea is not allowed to be null!");
            }
            else if (entity.Price == default)
            {
                throw new ValidationException("The field 'Price' of EventArea is not allowed to be null!");
            }
            else
            {
                throw new ValidationException("The field 'Description' of EventArea is not allowed to be empty!");
            }
        }
    }
}
