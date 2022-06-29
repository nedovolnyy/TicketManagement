using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IEventSeatService : IService<IEventSeat>
    {
        Task Validate(IEventSeat entity);
    }
}
