using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IEventSeatService : IService<EventSeat>
    {
        Task Validate(EventSeat entity);
    }
}
