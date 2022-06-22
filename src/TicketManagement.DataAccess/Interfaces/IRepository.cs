using System.Collections.Generic;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        /// <summary>
        /// Base method for insert data.
        /// </summary>
        /// <param name="entity">Entity.</param>
        int Insert(T entity);

        /// <summary>
        /// Base method for update data.
        /// </summary>
        /// <param name="entity">Entity.</param>
        int Update(T entity);

        /// <summary>
        /// Base method for delete data.
        /// </summary>
        /// <param name="id">id.</param>
        int Delete(int id);

        /// <summary>
        /// Base method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="BaseEntity"/>&lt;BaseEntity&gt;.</returns>
        T GetById(int id);

        /// <summary>
        /// Base method for populate all data.
        /// </summary>
        /// <returns><see cref="BaseEntity"/>&lt;BaseEntity&gt;.</returns>
        IEnumerable<T> GetAll();
    }
}
