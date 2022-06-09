using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal abstract class BaseRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly IDatabaseContext _databaseContext;
        protected BaseRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        /// <summary>
        /// Base method for insert data.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public int Insert(T entity)
        {
            int i;

            var cmd = _databaseContext.Connection.CreateCommand();
            AddParamsForInsert(entity, cmd);
            i = cmd.ExecuteNonQuery();
            return i;
        }

        /// <summary>
        /// Base method for update data.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public int Update(T entity)
        {
            int i;

            var cmd = _databaseContext.Connection.CreateCommand();
            AddParamsForUpdate(entity, cmd);
            i = cmd.ExecuteNonQuery();
            return i;
        }

        /// <summary>
        /// Base method for delete data.
        /// </summary>
        /// <param name="id">id.</param>
        public int Delete(int id)
        {
            int i;

            var cmd = _databaseContext.Connection.CreateCommand();
            AddParamsForDelete(id, cmd);
            i = cmd.ExecuteNonQuery();
            return i;
        }

        /// <summary>
        /// Base method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="BaseEntity"/>BaseEntity&gt;.</returns>
        public T GetById(int id)
        {
            var cmd = _databaseContext.Connection.CreateCommand();
            AddParamsForGetById(id, cmd);
            using var reader = cmd.ExecuteReader();
            return Map(reader);
        }

        /// <summary>
        /// Base method for populate all data.
        /// </summary>
        /// <returns><see cref="BaseEntity"/>List&lt;BaseEntity&gt;.</returns>
        public IEnumerable<T> GetAll()
        {
            var cmd = _databaseContext.Connection.CreateCommand();
            GetAllCommandParameters(cmd);
            using var reader = cmd.ExecuteReader();
            return Maps(reader);
        }

        protected abstract void AddParamsForInsert(T entity, SqlCommand cmd);
        protected abstract void AddParamsForUpdate(T entity, SqlCommand cmd);
        protected abstract void AddParamsForDelete(int id, SqlCommand cmd);
        protected abstract void AddParamsForGetById(int id, SqlCommand cmd);
        protected abstract void GetAllCommandParameters(SqlCommand cmd);
        protected abstract T Map(SqlDataReader reader);
        protected abstract List<T> Maps(SqlDataReader reader);
    }
}
