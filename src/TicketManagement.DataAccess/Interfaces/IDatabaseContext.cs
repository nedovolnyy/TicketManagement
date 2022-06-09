using System;
using System.Data.SqlClient;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IDatabaseContext
    {
        SqlConnection Connection { get; }
    }
}