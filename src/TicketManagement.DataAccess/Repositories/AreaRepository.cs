using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<Area> _dbSet;
        public AreaRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Areas;
        }

        public virtual IQueryable<Area> GetAllByLayoutId(int id)
            => _databaseContext.Areas.Where(p => p.LayoutId == id).AsQueryable();
    }
}
