using TicketManagement.Common.Entities;
using TicketManagement.DI;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventAreaRepository : BaseRepository<EventArea>, IEventAreaRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public EventAreaRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }
    }
}
