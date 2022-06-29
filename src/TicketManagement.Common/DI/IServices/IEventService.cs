using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IEventService : IService<IEvent>
    {
        Task Validate(IEvent entity);
        Task<int> GetSeatsAvailableCount(int id);
        Task<int> GetSeatsCount(int layoutId);
    }
}
