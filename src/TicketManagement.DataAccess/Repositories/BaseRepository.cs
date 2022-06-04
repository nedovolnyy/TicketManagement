using System;
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
        /// Base Method for Insert Data.
        /// </summary>
        /// <param name="entity">entity.</param>
        /// <returns>Count changed columns.</returns>
        public int Insert(T entity)
        {
            int i = 0;
            try
            {
                using (SqlConnection sqlConnection = new DatabaseContext().Connection)
                {
                    using (var sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (var cmd = sqlConnection.CreateCommand())
                        {
                            cmd.CommandText = ActionToSqlString('I');
                            cmd.CommandType = CommandType.Text;
                            InsertCommandParameters(entity, cmd);
                            cmd.Transaction = sqlTransaction;
                            i = cmd.ExecuteNonQuery();
                            sqlTransaction.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        /// <summary>
        /// Base Method for Update Data.
        /// </summary>
        /// <param name="entity">entity.</param>
        /// <returns>Count changed columns.</returns>
        public int Update(T entity)
        {
            int i = 0;
            try
            {
                using (SqlConnection sqlConnection = new DatabaseContext().Connection)
                {
                    using (var sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (var cmd = sqlConnection.CreateCommand())
                        {
                            cmd.CommandText = ActionToSqlString('U');
                            cmd.CommandType = CommandType.Text;
                            UpdateCommandParameters(entity, cmd);
                            cmd.Transaction = sqlTransaction;
                            i = cmd.ExecuteNonQuery();

                            sqlTransaction.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        /// <summary>
        /// Base Method for Delete Data.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>Count changed columns.</returns>
        public int Delete(int id)
        {
            int i = 0;
            try
            {
                using (SqlConnection sqlConnection = new DatabaseContext().Connection)
                {
                    using (var sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        using (var cmd = sqlConnection.CreateCommand())
                        {
                            cmd.CommandText = ActionToSqlString('D');
                            cmd.CommandType = CommandType.Text;
                            DeleteCommandParameters(id, cmd);
                            cmd.Transaction = sqlTransaction;
                            i = cmd.ExecuteNonQuery();

                            sqlTransaction.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        /// <summary>
        /// Base Method for Populate Data by key.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>Get Entity by Id.</returns>
        public T GetById(int id)
        {
            try
            {
                using (SqlConnection sqlConnection = new DatabaseContext().Connection)
                {
                    using (var cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandText = ActionToSqlString('G');
                        cmd.CommandType = CommandType.Text;
                        GetByIdCommandParameters(id, cmd);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            return Map(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Base Method for Populate All Data.
        /// </summary>
        /// <returns>Get all.</returns>
        public IEnumerable<T> GetAll()
        {
            try
            {
                using (SqlConnection sqlConnection = new DatabaseContext().Connection)
                {
                    using (var cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandText = ActionToSqlString('A');
                        cmd.CommandType = CommandType.Text;
                        GetAllCommandParameters(cmd);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            return Maps(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected abstract string ActionToSqlString(char action);
        protected abstract void InsertCommandParameters(T entity, SqlCommand cmd);
        protected abstract void UpdateCommandParameters(T entity, SqlCommand cmd);
        protected abstract void DeleteCommandParameters(int id, SqlCommand cmd);
        protected abstract void GetByIdCommandParameters(int id, SqlCommand cmd);
        protected abstract void GetAllCommandParameters(SqlCommand cmd);
        protected abstract T Map(SqlDataReader reader);
        protected abstract List<T> Maps(SqlDataReader reader);
    }
}
