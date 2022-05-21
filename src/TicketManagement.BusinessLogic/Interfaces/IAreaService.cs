using TicketManagement.BusinessLogic.DTO;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IAreaService
    {
        AreaDto GetArea(int id);
    }
}
