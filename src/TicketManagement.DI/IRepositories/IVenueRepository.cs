using TicketManagement.Common.Entities;

namespace TicketManagement.DI
{
    public interface IVenueRepository : IRepository<Venue>
    {
        /// <summary>
        /// Method for populate data by name.
        /// </summary>
        /// <param name="name">id.</param>
        /// <returns>First id(<see cref="int"/>), if in table Venue have same name.</returns>
        Task<int> GetIdFirstByName(string name);
    }
}
