using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;

namespace TicketManagement.Common.DI
{
    public interface IService<T>
        where T : BaseEntity
    {
        /// <summary>
        /// Base method for insert data.
        /// </summary>
        /// <param name="entity">Entity.</param>
        Task InsertAsync(T entity);

        /// <summary>
        /// Base method for update data.
        /// </summary>
        /// <param name="entity">Entity.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Base method for delete data.
        /// </summary>
        /// <param name="id">id.</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Base method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="BaseEntity"/>.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Base method for populate all data.
        /// </summary>
        /// <returns>List&lt;<see cref="BaseEntity"/>&gt;.</returns>
        Task<IEnumerable<T>> GetAllAsync();
    }
}
