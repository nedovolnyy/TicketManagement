using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IAreaService : IService<Area>
    {
        Task Validate(Area entity);
    }
}
