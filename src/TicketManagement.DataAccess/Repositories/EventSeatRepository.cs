using System.Collections.Generic;
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

        public override async Task<int> Insert(IEventSeat entity)
        {
            var state = (int)(await _dbSet.AddAsync((EventSeat)entity)).State;
            await _databaseContext.Instance.SaveChangesAsync();
            return state;
        }

        public override async Task<int> Delete(int id)
        {
            var state = (int)_dbSet.Remove(await _dbSet.FindAsync(id)).State;
            await _databaseContext.Instance.SaveChangesAsync();
            return state;
        }

        public override async Task<IEventSeat> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public override async Task<IEnumerable<IEventSeat>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
