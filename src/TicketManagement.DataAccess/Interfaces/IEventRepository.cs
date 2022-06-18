using System.Collections.Generic;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        IEnumerable<Event> GetAllByLayoutId(int layoutId);
        int GetCountEmptySeats(int id);
        int GetCountSeats(int layoutId);
    }
}
