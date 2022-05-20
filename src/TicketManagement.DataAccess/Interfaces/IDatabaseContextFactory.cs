using System;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IDatabaseContextFactory : IDisposable
    {
        IDatabaseContext Context();
    }
}
