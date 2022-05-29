using System.Collections.Generic;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IAreaRepository : IRepository<Area>
    {
        IEnumerable<Area> GetAllByLayoutId(int? id);
    }
}
