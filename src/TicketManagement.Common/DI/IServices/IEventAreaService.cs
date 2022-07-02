using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IEventAreaService : IService<IEventArea>
    {
        Task ValidateAsync(IEventArea entity);
    }
}
