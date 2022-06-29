using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IEventAreaService : IService<IEventArea>
    {
        Task Validate(IEventArea entity);
    }
}
