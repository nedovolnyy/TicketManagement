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
            if ((entity.EventId == 0) | (entity.CoordX == 0) | (entity.CoordY == 0) | (entity.Description == "") | (entity.Price == 0m))
            {
                throw new ValidationException("The field of EventArea is not allowed to be null!");
            }
        }
    }
}
