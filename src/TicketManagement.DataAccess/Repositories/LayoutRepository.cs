﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class LayoutRepository : BaseRepository<Layout>, ILayoutRepository
    {
        private readonly IDatabaseContext _databaseContext;

        internal LayoutRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        async Task<IEnumerable<Layout>> ILayoutRepository.GetAllByVenueId(int id)
            => await _databaseContext.Layouts.Where(p => p.VenueId == id).ToListAsync();
    }
}
