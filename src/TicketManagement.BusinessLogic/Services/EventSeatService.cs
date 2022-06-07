using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    internal class EventSeatService : BaseService<EventSeat>, IEventSeatService
    {
        private readonly IEventSeatRepository _eventSeatRepository;

        internal EventSeatService()
            : base(new EventSeatRepository())
        {
            _eventSeatRepository = new EventSeatRepository();
        }

        public EventSeatService(IEventSeatRepository eventSeatRepository)
            : base(eventSeatRepository)
        {
            _eventSeatRepository = eventSeatRepository;
        }

        public override void Validate(EventSeat entity)
        {
            if ((entity.EventAreaId == 0) | (entity.Row == 0) | (entity.Number == 0) | (entity.State == 0))
            {
                throw new ValidationException("The field of EventSeat is not allowed to be null!", "");
            }
        }
    }
}
