using TicketManagement.BusinessLogic.DTO;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface ILayoutService
    {
        LayoutDto GetLayout(int id);
    }
}
