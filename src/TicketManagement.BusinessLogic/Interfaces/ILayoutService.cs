using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface ILayoutService : IService<Layout>
    {
        void Validate(Layout entity);
    }
}
