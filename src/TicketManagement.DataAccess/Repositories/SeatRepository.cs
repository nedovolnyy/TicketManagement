using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class SeatRepository : BaseRepository<ISeat>, ISeatRepository
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<Seat> _dbSet;

        public SeatRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Seats;
        }

        public override async Task<int> Insert(ISeat entity)
        {
            var state = (int)(await _dbSet.AddAsync((Seat)entity)).State;
            await _databaseContext.Instance.SaveChangesAsync();
            return state;
        }

        public override async Task<int> Delete(int id)
        {
            var state = (int)_dbSet.Remove(await _dbSet.FindAsync(id)).State;
            await _databaseContext.Instance.SaveChangesAsync();
            return state;
        }

        public override async Task<ISeat> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public override async Task<IEnumerable<ISeat>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        async Task<IEnumerable<ISeat>> ISeatRepository.GetAllByAreaId(int id)
            => await _dbSet.Where(p => p.AreaId == id).ToListAsync();
    }
}
