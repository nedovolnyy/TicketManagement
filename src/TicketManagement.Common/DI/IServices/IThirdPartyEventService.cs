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
        /// <param name="evnt">evnt.</param>
        /// <param name="price">price.</param>
        /// <param name="evntLogoImage">evntLogoImage.</param>
        Task InsertEventToDatabase(string fullImagePath, Event evnt, decimal price, string evntLogoImage);
    }
}
