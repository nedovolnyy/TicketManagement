using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IAreaService : IService<Area>
    {
        void Validate(Area entity);
    }
}
