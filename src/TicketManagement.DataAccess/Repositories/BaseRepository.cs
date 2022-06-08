using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.ADO;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal abstract class BaseRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        /// <summary>
        /// Base method for insert data.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public int Insert(T entity)
        {
            int i;

            using var sqlConnection = new DatabaseContext().Connection;
            using var sqlTransaction = sqlConnection.BeginTransaction();
            using var cmd = sqlConnection.CreateCommand();
            cmd.CommandText = GetSQLStatement("Insert");
            cmd.CommandType = CommandType.Text;
            AddParamsForInsert(entity, cmd);
            cmd.Transaction = sqlTransaction;
            i = cmd.ExecuteNonQuery();
            sqlTransaction.Commit();

            return i;
        }

        /// <summary>
        /// Base method for update data.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public int Update(T entity)
        {
            int i;

            using var sqlConnection = new DatabaseContext().Connection;
            using var sqlTransaction = sqlConnection.BeginTransaction();
            using var cmd = sqlConnection.CreateCommand();
            cmd.CommandText = GetSQLStatement("Update");
            cmd.CommandType = CommandType.Text;
            AddParamsForUpdate(entity, cmd);
            cmd.Transaction = sqlTransaction;
            i = cmd.ExecuteNonQuery();
            sqlTransaction.Commit();

            return i;
        }

        /// <summary>
        /// Base method for delete data.
        /// </summary>
        /// <param name="id">id.</param>
        public int Delete(int id)
        {
            int i;

            using var sqlConnection = new DatabaseContext().Connection;
            using var sqlTransaction = sqlConnection.BeginTransaction();
            using var cmd = sqlConnection.CreateCommand();
            cmd.CommandText = GetSQLStatement("Delete");
            cmd.CommandType = CommandType.Text;
            AddParamsForDelete(id, cmd);
            cmd.Transaction = sqlTransaction;
            i = cmd.ExecuteNonQuery();
            sqlTransaction.Commit();

            return i;
        }

        /// <summary>
        /// Base method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="BaseEntity"/>BaseEntity&gt;.</returns>
        public T GetById(int id)
        {
            using var cmd = new DatabaseContext().Connection.CreateCommand();
            cmd.CommandText = GetSQLStatement("GetById");
            cmd.CommandType = CommandType.Text;
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
            using var cmd = new DatabaseContext().Connection.CreateCommand();
            cmd.CommandText = GetSQLStatement("GetAll");
            cmd.CommandType = CommandType.Text;
            GetAllCommandParameters(cmd);
            using var reader = cmd.ExecuteReader();
            return Maps(reader);
        }

        protected abstract string GetSQLStatement(string action);
        protected abstract void AddParamsForInsert(T entity, SqlCommand cmd);
        protected abstract void AddParamsForUpdate(T entity, SqlCommand cmd);
        protected abstract void AddParamsForDelete(int id, SqlCommand cmd);
        protected abstract void AddParamsForGetById(int id, SqlCommand cmd);
        protected abstract void GetAllCommandParameters(SqlCommand cmd);
        protected abstract T Map(SqlDataReader reader);
        protected abstract List<T> Maps(SqlDataReader reader);
    }
}
