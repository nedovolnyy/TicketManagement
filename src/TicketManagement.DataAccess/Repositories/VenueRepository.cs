using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class VenueRepository : BaseRepository<Venue>, IVenueRepository
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<Venue> _dbSet;

        public VenueRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Venues;
        }

        public async Task<int> GetIdFirstByNameAsync(string name)
        {
            var tmpVenue = await _databaseContext.Venues.Where(p => p.Name == name).FirstOrDefaultAsync();
            if (tmpVenue is null)
            {
                return default;
            }

            return tmpVenue.Id;
        }
    }
}
