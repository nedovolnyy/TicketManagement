using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IVenueRepository : IRepository<Venue>
    {
        /// <summary>
        /// Method for populate data by name.
        /// </summary>
        /// <param name="name">id.</param>
        /// <returns>First id(<see cref="int"/>), if in table Venue have same name.</returns>
        int GetIdFirstByName(string name);
    }
}
