using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class LayoutRepository : BaseRepository<Layout>, ILayoutRepository
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<Layout> _dbSet;

        public LayoutRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Layouts;
        }

        public IQueryable<Layout> GetAllByVenueId(int venueId)
            => _databaseContext.Layouts.Where(p => p.VenueId == venueId).AsNoTracking();
    }
}
