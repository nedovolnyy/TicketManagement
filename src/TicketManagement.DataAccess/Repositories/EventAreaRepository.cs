using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventAreaRepository : BaseRepository<IEventArea>, IEventAreaRepository
    {
        private readonly DbSet<EventArea> _dbSet;

        public EventAreaRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _dbSet = databaseContext.EventAreas;
        }

        public override async Task<int> Insert(IEventArea entity)
        {
            await _dbSet.AddAsync((EventArea)entity);
            return await base.Insert(entity);
        }

        public override async Task<int> Delete(int id)
        {
            var i = (int)_dbSet.Remove(await _dbSet.FindAsync(id)).State;
            await base.Delete(i);
            return i;
        }

        public override async Task<IEventArea> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public override async Task<IEnumerable<IEventArea>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
