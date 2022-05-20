using System;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.ADO
{
    public class DatabaseContextFactory : IDatabaseContextFactory
    {
        private IDatabaseContext _dataContext;

        /// <summary>
        /// If data context remain null then initialize new context.
        /// </summary>
        /// <returns>dataContext.</returns>
        public IDatabaseContext Context()
        {
            return _dataContext ?? (_dataContext = new DatabaseContext());
        }

        /// <summary>
        /// Dispose existing context.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_dataContext != null)
            {
                _dataContext.Dispose();
            }
        }
    }
}
