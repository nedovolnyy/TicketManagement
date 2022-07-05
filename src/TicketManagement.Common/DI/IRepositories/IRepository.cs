using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IRepository<T>
        where T : class, IBaseEntity
    {
        /// <summary>
        /// Base method for insert data.
        /// </summary>
        /// <param name="entity">Entity.</param>
        Task<int> InsertAsync(T entity);

        /// <summary>
        /// Base method for update data.
        /// </summary>
        /// <param name="entity">Entity.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Base method for delete data.
        /// </summary>
        /// <param name="id">id.</param>
        Task<int> DeleteAsync(int id);

        /// <summary>
        /// Base method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="IBaseEntity"/>.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Base method for populate all data.
        /// </summary>
        /// <returns>List&lt;<see cref="IBaseEntity"/>&gt;.</returns>
        IQueryable<T> GetAll();
    }
}
