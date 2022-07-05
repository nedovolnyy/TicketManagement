﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IService<T>
        where T : class, IBaseEntity
    {
        /// <summary>
        /// Base method for insert data.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <returns><see cref="int"/>.</returns>
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
        /// <returns><see cref="int"/>.</returns>
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
        Task<IEnumerable<T>> GetAllAsync();
    }
}
