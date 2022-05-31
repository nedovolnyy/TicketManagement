using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class EventAreaService : BaseService<EventArea>, IService<EventArea>
    {
        private readonly IEventAreaRepository _eventAreaRepository;
        public EventAreaService()
            : base()
        {
            EntityRepository = new EventAreaRepository();
            _eventAreaRepository = (IEventAreaRepository)EntityRepository;
        }

        protected override IRepository<EventArea> EntityRepository { get; }

        protected override void Validation(EventArea entity)
        {
        }
    }
}
