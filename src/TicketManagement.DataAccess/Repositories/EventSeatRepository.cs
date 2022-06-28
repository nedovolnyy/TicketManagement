using TicketManagement.Common.Entities;
using TicketManagement.DI;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventSeatRepository : BaseRepository<EventSeat>, IEventSeatRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public EventSeatRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }
    }
}
