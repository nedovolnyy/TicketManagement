using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface ILayoutService : IService<ILayout>
    {
        Task Validate(ILayout entity);
    }
}
