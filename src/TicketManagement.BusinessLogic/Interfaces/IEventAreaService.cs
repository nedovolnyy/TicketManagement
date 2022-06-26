using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IEventAreaService : IService<EventArea>
    {
        Task Validate(EventArea entity);
    }
}
