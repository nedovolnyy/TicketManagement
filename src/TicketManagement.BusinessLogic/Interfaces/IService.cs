using System.Collections.Generic;
using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IService<T>
        where T : BaseEntity
    {
        void Insert(T dto);
        void Update(T dto);
        void Delete(int id);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
