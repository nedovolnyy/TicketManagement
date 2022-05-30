using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class EventAreaService : BaseService<EventArea>, IService<EventArea>
    {
        private readonly EventAreaRepository _eventAreaRepository;
        public EventAreaService()
            : base()
        {
            EntityRepository = new EventAreaRepository();
            _eventAreaRepository = (EventAreaRepository)EntityRepository;
        }

        protected override BaseRepository<EventArea> EntityRepository { get; }

        protected override void Validation(EventArea entity)
        {
        }
    }
}
