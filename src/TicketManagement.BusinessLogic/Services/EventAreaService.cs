using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    internal class EventAreaService : BaseService<EventArea>, IEventAreaService
    {
        private readonly IEventAreaRepository _eventAreaRepository;

        internal EventAreaService()
            : base(new EventAreaRepository())
        {
            _eventAreaRepository = new EventAreaRepository();
        }

        public EventAreaService(IEventAreaRepository eventAreaRepository)
            : base(eventAreaRepository)
        {
            _eventAreaRepository = eventAreaRepository;
        }

        public override void Validate(EventArea entity)
        {
        }
    }
}
