using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IEventService : IService<IEvent>
    {
        Task ValidateAsync(IEvent entity);
        Task<int> GetSeatsAvailableCountAsync(int id);
        Task<int> GetSeatsCountAsync(int layoutId);
    }
}
