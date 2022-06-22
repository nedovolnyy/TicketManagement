using System.Collections.Generic;
using System.Data.Common;
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

        public int Insert(T entity)
        {
            int i;

            var cmd = _databaseContext.Connection.CreateCommand();
            AddParamsForInsert(entity, cmd);
            i = cmd.ExecuteNonQuery();
            return i;
        }

        public int Update(T entity)
        {
            int i;

            var cmd = _databaseContext.Connection.CreateCommand();
            AddParamsForUpdate(entity, cmd);
            i = cmd.ExecuteNonQuery();
            return i;
        }

        public int Delete(int id)
        {
            int i;

            var cmd = _databaseContext.Connection.CreateCommand();
            AddParamsForDelete(id, cmd);
            i = cmd.ExecuteNonQuery();
            return i;
        }

        public T GetById(int id)
        {
            var cmd = _databaseContext.Connection.CreateCommand();
            AddParamsForGetById(id, cmd);
            using var reader = cmd.ExecuteReader();
            return Map(reader);
        }

        public IEnumerable<T> GetAll()
        {
            var cmd = _databaseContext.Connection.CreateCommand();
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
