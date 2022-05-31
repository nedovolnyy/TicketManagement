using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class EventSeatService : BaseService<EventSeat>, IService<EventSeat>
    {
        private readonly IEventSeatRepository _eventSeatRepository;
        public EventSeatService()
            : base()
        {
            EntityRepository = new EventSeatRepository();
            _eventSeatRepository = (IEventSeatRepository)EntityRepository;
        }

        protected override IRepository<EventSeat> EntityRepository { get; }

        protected override void Validation(EventSeat entity)
        {
            if ((entity.Row == null) || (entity.Number == null))
            {
                throw new ValidationException("Event can't be created without any seats!", "");
            }
        }
    }
}
