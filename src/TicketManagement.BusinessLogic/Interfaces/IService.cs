using TicketManagement.BusinessLogic.DTO;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IService<T>
        where T : BaseDto
    {
        T Get(int id);
    }
}
