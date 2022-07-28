using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IThirdPartyEventService
    {
        /// <summary>
        /// Method for insert ThirdPartyEvent to Database.
        /// </summary>
        /// <param name="eventFromJson">EventFromJson.</param>
        Task InsertAsync(EventFromJson eventFromJson);
    }
}
