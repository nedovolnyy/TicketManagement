using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IService<T>
        where T : BaseEntity
    {
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(int id);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
