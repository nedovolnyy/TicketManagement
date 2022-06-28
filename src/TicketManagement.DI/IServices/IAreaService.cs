using TicketManagement.Common.Entities;

namespace TicketManagement.DI
{
    public interface IAreaService : IService<Area>
    {
        Task Validate(Area entity);
    }
}
