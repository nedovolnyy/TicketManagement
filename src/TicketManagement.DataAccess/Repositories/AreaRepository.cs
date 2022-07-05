using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class AreaRepository : BaseRepository<IArea>, IAreaRepository
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<Area> _dbSet;
        public AreaRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Areas;
        }

        public override async Task<int> InsertAsync(IArea entity)
        {
            var state = (int)(await _dbSet.AddAsync((Area)entity)).State;
            await _databaseContext.Instance.SaveChangesAsync();
            return state;
        }

        public override async Task<int> DeleteAsync(int id)
        {
            var state = (int)_dbSet.Remove(await _dbSet.FindAsync(id)).State;
            await _databaseContext.Instance.SaveChangesAsync();
            return state;
        }

        public override async Task<IArea> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public override IQueryable<IArea> GetAll()
            => _dbSet.AsQueryable();

        public virtual IQueryable<IArea> GetAllByLayoutId(int id)
            => _databaseContext.Areas.Where(p => p.LayoutId == id).AsQueryable();
    }
}
