using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IEventService : IService<Event>
    {
        Task Validate(Event entity);
        Task<int> GetSeatsAvailableCount(int id);
        Task<int> GetSeatsCount(int layoutId);
    }
}
