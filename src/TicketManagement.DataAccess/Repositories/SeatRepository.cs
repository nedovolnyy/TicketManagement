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
        private readonly DbSet<Seat> _dbSet;

        public SeatRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _dbSet = databaseContext.Seats;
        }

        public override async Task<int> Insert(ISeat entity)
        {
            await _dbSet.AddAsync((Seat)entity);
            return await base.Insert(entity);
        }

        public override async Task<int> Delete(int id)
        {
            var i = (int)_dbSet.Remove(await _dbSet.FindAsync(id)).State;
            await base.Delete(i);
            return i;
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
