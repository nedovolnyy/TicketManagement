using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventAreaRepository : BaseRepository<EventArea>, IEventAreaRepository
    {
        private readonly IDatabaseContext _databaseContext;

        internal EventAreaRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }
    }
}
