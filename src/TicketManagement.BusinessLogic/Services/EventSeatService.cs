using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    internal class EventSeatService : BaseService<EventSeat>
    {
        private readonly IEventSeatRepository _eventSeatRepository;
        internal EventSeatService()
        {
            EntityRepository = new EventSeatRepository();
            _eventSeatRepository = (IEventSeatRepository)EntityRepository;
        }

        protected override IRepository<EventSeat> EntityRepository { get; set; }

        protected override void Validate(EventSeat entity)
        {
            if ((entity.Row == 0) || (entity.Number == 0))
            {
                throw new ValidationException("Event can't be created without any seats!", "");
            }
        }
    }
}
