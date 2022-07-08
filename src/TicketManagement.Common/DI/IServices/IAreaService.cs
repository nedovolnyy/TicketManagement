using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IAreaService : IService<Area>
    {
        Task ValidateAsync(Area entity);
    }
}
