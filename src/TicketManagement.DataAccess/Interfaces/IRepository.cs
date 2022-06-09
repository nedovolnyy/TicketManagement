using System.Collections.Generic;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        int Insert(T entity);
        int Update(T entity);
        int Delete(int id);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
