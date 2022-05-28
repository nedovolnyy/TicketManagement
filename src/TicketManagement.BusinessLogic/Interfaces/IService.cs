using System.Collections.Generic;
using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IService<T>
        where T : BaseEntity
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        void Delete(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
