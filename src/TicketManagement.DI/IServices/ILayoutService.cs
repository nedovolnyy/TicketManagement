using TicketManagement.Common.Entities;

namespace TicketManagement.DI
{
    public interface ILayoutService : IService<Layout>
    {
        Task Validate(Layout entity);
    }
}
