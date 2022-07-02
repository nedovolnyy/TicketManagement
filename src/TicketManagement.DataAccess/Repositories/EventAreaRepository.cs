using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventAreaRepository : BaseRepository<IEventArea>, IEventAreaRepository
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<EventArea> _dbSet;

        public EventAreaRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.EventAreas;
        }

        public override async Task<int> InsertAsync(IEventArea entity)
        {
            var state = (int)(await _dbSet.AddAsync((EventArea)entity)).State;
            await _databaseContext.Instance.SaveChangesAsync();
            return state;
        }

        public override async Task<int> DeleteAsync(int id)
        {
            var state = (int)_dbSet.Remove(await _dbSet.FindAsync(id)).State;
            await _databaseContext.Instance.SaveChangesAsync();
            return state;
        }

        public override async Task<IEventArea> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public override IQueryable<IEventArea> GetAll()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
