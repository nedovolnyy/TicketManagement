using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventSeatRepository : BaseRepository<IEventSeat>, IEventSeatRepository
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<EventSeat> _dbSet;

        public EventSeatRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.EventSeats;
        }

        public override async Task<int> InsertAsync(IEventSeat entity)
        {
            var state = (int)(await _dbSet.AddAsync((EventSeat)entity)).State;
            await _databaseContext.Instance.SaveChangesAsync();
            return state;
        }

        public override async Task<int> DeleteAsync(int id)
        {
            var state = (int)_dbSet.Remove(await _dbSet.FindAsync(id)).State;
            await _databaseContext.Instance.SaveChangesAsync();
            return state;
        }

        public override async Task<IEventSeat> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public override IQueryable<IEventSeat> GetAll()
            => _dbSet.AsNoTracking();

        public virtual IQueryable<IEventSeat> GetAllByEventAreaId(int eventAreaId)
            => _databaseContext.EventSeats.Where(p => p.EventAreaId == eventAreaId).AsQueryable();
    }
}
