using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface ILayoutService : IService<Layout>
    {
        Task Validate(Layout entity);
    }
}
