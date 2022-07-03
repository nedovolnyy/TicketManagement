﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IVenueRepository : IRepository<IVenue>
    {
        /// <summary>
        /// Method for populate data by name.
        /// </summary>
        /// <param name="name">id.</param>
        /// <returns>First id(<see cref="int"/>), if in table Venue have same name.</returns>
        Task<int> GetIdFirstByNameAsync(string name);
    }
}
