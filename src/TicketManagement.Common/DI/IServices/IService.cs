using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IService<T>
        where T : class, IBaseEntity
    {
        Task<int> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Base method for populate all data.
        /// </summary>
        /// <returns>List&lt;<see cref="IBaseEntity"/>&gt;.</returns>
        Task<IEnumerable<T>> GetAllAsync();
    }
}
