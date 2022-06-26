﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class VenueRepository : BaseRepository<Venue>, IVenueRepository
    {
        private readonly IDatabaseContext _databaseContext;

        internal VenueRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<int> GetIdFirstByName(string name)
            => (await _databaseContext.Venues.Where(p => p.Name == name).FirstOrDefaultAsync()).Id;
    }
}
