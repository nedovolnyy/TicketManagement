using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IVenueService : IService<Venue>
    {
        Task Validate(Venue entity);
    }
}
