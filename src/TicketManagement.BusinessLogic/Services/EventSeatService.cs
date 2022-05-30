using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class EventSeatService : BaseService<EventSeat>, IService<EventSeat>
    {
        private readonly EventSeatRepository _eventSeatRepository;
        public EventSeatService()
            : base()
        {
            EntityRepository = new EventSeatRepository();
            _eventSeatRepository = (EventSeatRepository)EntityRepository;
        }

        protected override BaseRepository<EventSeat> EntityRepository { get; }

        protected override void Validation(EventSeat entity)
        {
            if ((entity.Row == null) || (entity.Number == null))
            {
                throw new ValidationException("Event can't be created without any seats!", "");
            }
        }
    }
}
