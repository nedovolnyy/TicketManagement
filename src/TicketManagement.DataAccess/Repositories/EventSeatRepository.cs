using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventSeatRepository : BaseRepository<IEventSeat>, IEventSeatRepository
    {
        private readonly DbSet<EventSeat> _dbSet;

        public EventSeatRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _dbSet = databaseContext.EventSeats;
        }

        public override async Task<int> Insert(IEventSeat entity)
        {
            await _dbSet.AddAsync((EventSeat)entity);
            return await base.Insert(entity);
        }

        public override async Task<int> Delete(int id)
        {
            var i = (int)_dbSet.Remove(await _dbSet.FindAsync(id)).State;
            await base.Delete(i);
            return i;
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
