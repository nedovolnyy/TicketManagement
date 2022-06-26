using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface ISeatService : IService<Seat>
    {
        Task Validate(Seat entity);
    }
}
