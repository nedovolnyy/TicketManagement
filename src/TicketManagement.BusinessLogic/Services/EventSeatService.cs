using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.Services
{
    internal class EventSeatService : BaseService<EventSeat>, IEventSeatService
    {
        public EventSeatService(IEventSeatRepository eventSeatRepository)
            : base(eventSeatRepository)
        {
        }

        public override void Validate(EventSeat entity)
        {
            if (entity.EventAreaId == default)
            {
                throw new ValidationException("The field 'EventAreaId' of EventSeat is not allowed to be null!");
            }
            else if (entity.Row == default)
            {
                throw new ValidationException("The field 'Row' of EventSeat is not allowed to be null!");
            }
            else if (entity.Number == default)
            {
                throw new ValidationException("The field 'Number' of EventSeat is not allowed to be null!");
            }
            else
            {
                throw new ValidationException("The field 'State' of EventSeat is not allowed to be null!");
            }
        }
    }
}
