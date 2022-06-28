using TicketManagement.Common.Entities;

namespace TicketManagement.DI
{
    public interface ISeatService : IService<Seat>
    {
        Task Validate(Seat entity);
    }
}
