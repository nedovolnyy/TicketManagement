using TicketManagement.Common.Entities;

namespace TicketManagement.DI
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        /// <summary>
        /// Base method for insert data.
        /// </summary>
        /// <param name="entity">Entity.</param>
        Task<int> Insert(T entity);

        /// <summary>
        /// Base method for update data.
        /// </summary>
        /// <param name="entity">Entity.</param>
        Task<int> Update(T entity);

        /// <summary>
        /// Base method for delete data.
        /// </summary>
        /// <param name="id">id.</param>
        Task<int> Delete(int id);

        /// <summary>
        /// Base method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="BaseEntity"/>&lt;BaseEntity&gt;.</returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Base method for populate all data.
        /// </summary>
        /// <returns><see cref="BaseEntity"/>&lt;BaseEntity&gt;.</returns>
        Task<IEnumerable<T>> GetAll();
    }
}
