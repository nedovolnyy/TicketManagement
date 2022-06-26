﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
        private readonly IDatabaseContext _databaseContext;

        internal AreaRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        async Task<IEnumerable<Area>> IAreaRepository.GetAllByLayoutId(int id)
            => await _databaseContext.Areas.Where(p => p.LayoutId == id).ToListAsync();
    }
}
