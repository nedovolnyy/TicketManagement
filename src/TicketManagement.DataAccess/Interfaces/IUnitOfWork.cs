using System.Data.SqlClient;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        IDatabaseContext DataContext { get; }
        SqlTransaction BeginTransaction();

        /// <summary>
        /// The Commit.
        /// </summary>
        void Commit();
    }
}
