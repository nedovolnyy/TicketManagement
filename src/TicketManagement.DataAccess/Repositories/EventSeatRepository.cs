using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventSeatRepository : BaseRepository<EventSeat>, IEventSeatRepository
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<EventSeat> _dbSet;

        public EventSeatRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.EventSeats;
        }

        public async Task ChangeEventSeatStatusAsync(int eventSeatId)
        {
            var eventSeat = await GetByIdAsync(eventSeatId);
            eventSeat.State = !eventSeat.State;
            await UpdateAsync(eventSeat);
        }

        public virtual IQueryable<EventSeat> GetAllByEventAreaId(int eventAreaId)
            => _databaseContext.EventSeats.Where(p => p.EventAreaId == eventAreaId).AsQueryable();
    }
}
