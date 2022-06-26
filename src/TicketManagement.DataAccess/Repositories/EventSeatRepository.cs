using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventSeatRepository : BaseRepository<EventSeat>, IEventSeatRepository
    {
        private readonly IDatabaseContext _databaseContext;

        internal EventSeatRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }
    }
}
