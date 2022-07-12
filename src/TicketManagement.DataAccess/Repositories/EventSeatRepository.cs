using System.Linq;
using System.Threading.Tasks;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

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

        public async Task ChangeEventSeatStatusAsync(int eventSeatId, State state = State.Available)
        {
            var eventSeat = await GetByIdAsync(eventSeatId);
            if (state == State.Available)
            {
                eventSeat.State = eventSeat.State == State.Available ? State.NotAvailable : State.Available;
            }

            if (state != State.Available)
            {
                eventSeat.State = eventSeat.State == State.Available ? state : State.Available;
            }

            await UpdateAsync(eventSeat);
        }

        public virtual IQueryable<EventSeat> GetAllByEventAreaId(int eventAreaId)
            => _databaseContext.EventSeats.Where(p => p.EventAreaId == eventAreaId).AsQueryable();
    }
}
