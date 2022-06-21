using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.EF;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal abstract class BaseRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly DatabaseContext _databaseContext;
        protected BaseRepository(DatabaseContext databaseContext)
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

            var cmd = _databaseContext.Database.GetDbConnection().CreateCommand();
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

            var cmd = _databaseContext.Database.GetDbConnection().CreateCommand();
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

            var cmd = _databaseContext.Database.GetDbConnection().CreateCommand();
            AddParamsForDelete(id, cmd);
            i = cmd.ExecuteNonQuery();
            return i;
        }

        /// <summary>
        /// Base method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="BaseEntity"/>&lt;BaseEntity&gt;.</returns>
        public T GetById(int id)
        {
            var cmd = _databaseContext.Database.GetDbConnection().CreateCommand();
            AddParamsForGetById(id, cmd);
            using var reader = cmd.ExecuteReader();
            return Map(reader);
        }

        /// <summary>
        /// Base method for populate all data.
        /// </summary>
        /// <returns><see cref="BaseEntity"/>&lt;BaseEntity&gt;.</returns>
        public IEnumerable<T> GetAll()
        {
            var cmd = _databaseContext.Database.GetDbConnection().CreateCommand();
            GetAllCommandParameters(cmd);
            using var reader = cmd.ExecuteReader();
            return Maps(reader);
        }

        protected abstract void AddParamsForInsert(T entity, DbCommand cmd);
        protected abstract void AddParamsForUpdate(T entity, DbCommand cmd);
        protected abstract void AddParamsForDelete(int id, DbCommand cmd);
        protected abstract void AddParamsForGetById(int id, DbCommand cmd);
        protected abstract void GetAllCommandParameters(DbCommand cmd);
        protected abstract T Map(DbDataReader reader);
        protected abstract List<T> Maps(DbDataReader reader);
    }
}
