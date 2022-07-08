using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventAreaRepository : BaseRepository<EventArea>, IEventAreaRepository
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<EventArea> _dbSet;

        public EventAreaRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.EventAreas;
        }

        public virtual IQueryable<EventArea> GetAllByEventId(int eventId)
            => _databaseContext.EventAreas.Where(p => p.EventId == eventId).AsQueryable();
    }
}
