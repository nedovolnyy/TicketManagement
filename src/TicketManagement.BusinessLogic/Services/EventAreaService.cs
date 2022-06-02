using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    internal class EventAreaService : BaseService<EventArea>
    {
        private readonly IEventAreaRepository _eventAreaRepository;
        internal EventAreaService()
        {
            EntityRepository = new EventAreaRepository();
            _eventAreaRepository = (IEventAreaRepository)EntityRepository;
        }

        protected override IRepository<EventArea> EntityRepository { get; set; }

        protected override void Validate(EventArea entity)
        {
        }
    }
}
