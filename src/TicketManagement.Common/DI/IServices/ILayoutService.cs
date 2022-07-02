using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface ILayoutService : IService<ILayout>
    {
        Task ValidateAsync(ILayout entity);
    }
}
