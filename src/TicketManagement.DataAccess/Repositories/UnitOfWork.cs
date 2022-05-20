using System;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDatabaseContextFactory _factory;
        private IDatabaseContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// Constructor which will initialize the datacontext factory.
        /// </summary>
        /// <param name="factory">datacontext factory.</param>
        public UnitOfWork(IDatabaseContextFactory factory) => _factory = factory;

        /// <summary>
        /// Define a property of context class.
        /// </summary>
        public IDatabaseContext DataContext
        {
            get
            {
                return _context ??= _factory.Context();
            }
        }

        public SqlTransaction Transaction { get; private set; }

        /// <summary>
        /// Following method will use to Commit or Rollback memory data into database.
        /// </summary>
        void IUnitOfWork.Commit()
        {
            if (Transaction != null)
            {
                try
                {
                    Transaction.Commit();
                }
                catch (Exception)
                {
                    Transaction.Rollback();
                }

                Transaction.Dispose();
                Transaction = null;
            }
            else
            {
                NullReferenceException nullReferenceException = new NullReferenceException("Tryed commit not opened transaction");
                throw nullReferenceException;
            }
        }

        /// <summary>
        /// Begin a database transaction.
        /// </summary>
        /// <returns>Transaction.</returns>
        SqlTransaction IUnitOfWork.BeginTransaction()
        {
            if (Transaction != null)
            {
                NullReferenceException nullReferenceException = new NullReferenceException("Not finished previous transaction");
                throw nullReferenceException;
            }

            Transaction = _context.Connection.BeginTransaction();
            return Transaction;
        }

        /// <summary>
        /// dispose a Transaction.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
            }

            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
