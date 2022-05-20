using System;
using System.Data.SqlClient;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IDatabaseContext : IDisposable
    {
        SqlConnection Connection { get; }
    }
}
