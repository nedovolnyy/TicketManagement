using System.Data;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.ADO
{
    internal class DatabaseContext : IDatabaseContext
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        internal DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection Connection
        {
            get
            {
                _connection ??= new SqlConnection(_connectionString);
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                return _connection;
            }
        }
    }
}