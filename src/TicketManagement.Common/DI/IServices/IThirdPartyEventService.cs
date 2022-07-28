using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IThirdPartyEventService
    {
        /// <summary>
        /// Method for insert ThirdPartyEvent to Database.
        /// </summary>
        /// <param name="fullImagePath">fullImagePath.</param>
        /// <param name="event">event.</param>
        /// <param name="price">price.</param>
        /// <param name="evntLogoImage">evntLogoImage.</param>
        Task InsertAsync(string fullImagePath, Event @event, decimal price, string evntLogoImage);
    }
}
