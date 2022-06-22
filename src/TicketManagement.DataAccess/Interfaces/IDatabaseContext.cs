using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IDatabaseContext
    {
        DbContext Instance { get; }
        DbConnection Connection { get; }
    }
}