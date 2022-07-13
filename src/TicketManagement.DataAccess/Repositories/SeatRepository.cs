using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class SeatRepository : BaseRepository<Seat>, ISeatRepository
    {
        private readonly DbSet<Seat> _dbSet;

        public SeatRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _dbSet = databaseContext.Seats;
        }

        public IQueryable<Seat> GetAllByAreaId(int areaId)
            => _dbSet.Where(p => p.AreaId == areaId).AsNoTracking();
    }
}
