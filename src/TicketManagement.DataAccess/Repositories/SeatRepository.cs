using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class SeatRepository : BaseRepository<Seat>, ISeatRepository
    {
        private readonly IDatabaseContext _databaseContext;

        internal SeatRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        async Task<IEnumerable<Seat>> ISeatRepository.GetAllByAreaId(int id)
            => await _databaseContext.Seats.Where(p => p.AreaId == id).ToListAsync();
    }
}
