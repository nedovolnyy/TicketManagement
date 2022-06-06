using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface ISeatService : IService<Seat>
    {
        void Validate(Seat entity);
    }
}
