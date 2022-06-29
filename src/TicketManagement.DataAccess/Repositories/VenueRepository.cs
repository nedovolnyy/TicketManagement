using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class VenueRepository : BaseRepository<IVenue>, IVenueRepository
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<Venue> _dbSet;

        public VenueRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Venues;
        }

        public override async Task<int> Insert(IVenue entity)
        {
            await _dbSet.AddAsync((Venue)entity);
            return await base.Insert(entity);
        }

        public override async Task<int> Delete(int id)
        {
            var i = (int)_dbSet.Remove(await _dbSet.FindAsync(id)).State;
            await base.Delete(i);
            return i;
        }

        public override async Task<IVenue> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public override async Task<IEnumerable<IVenue>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<int> GetIdFirstByName(string name)
            => (await _databaseContext.Venues.Where(p => p.Name == name).FirstOrDefaultAsync()).Id;
    }
}
