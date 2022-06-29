using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class LayoutRepository : BaseRepository<ILayout>, ILayoutRepository
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<Layout> _dbSet;

        public LayoutRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Layouts;
        }

        public override async Task<int> Insert(ILayout entity)
        {
            var state = (int)(await _dbSet.AddAsync((Layout)entity)).State;
            await _databaseContext.Instance.SaveChangesAsync();
            return state;
        }

        public override async Task<int> Delete(int id)
        {
            var state = (int)_dbSet.Remove(await _dbSet.FindAsync(id)).State;
            await _databaseContext.Instance.SaveChangesAsync();
            return state;
        }

        public override async Task<ILayout> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public override async Task<IEnumerable<ILayout>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        async Task<IEnumerable<ILayout>> ILayoutRepository.GetAllByVenueId(int id)
            => await _databaseContext.Layouts.Where(p => p.VenueId == id).ToListAsync();
    }
}
