using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IService<T>
        where T : class, IBaseEntity
    {
        Task<int> Insert(T entity);
        Task Update(T entity);
        Task<int> Delete(int id);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
