using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.ADO;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : BaseEntity, new()
    {
        protected BaseRepository()
        {
        }

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
                            cmd.Transaction = sqlTransaction;
                            InsertCommandParameters(entity, cmd);
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
                            cmd.Transaction = sqlTransaction;
                            UpdateCommandParameters(entity, cmd);
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
                            cmd.Transaction = sqlTransaction;
                            DeleteCommandParameters(id, cmd);
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
        /// <param name="entity">id.</param>
        /// <returns>Count changed columns.</returns>
        public int Delete(T entity)
        {
            int i = 0;
            var tmpEntity = new T();
            try
            {
                using (SqlConnection sqlConnection = new DatabaseContext().Connection)
                {
                    using (var cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandText = ActionToSqlString('G');
                        cmd.CommandType = CommandType.Text;
                        GetByIdCommandParameters(entity.Id, cmd);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            tmpEntity = Map(reader);
                        }
                    }

                    if (entity.Equals(entity, tmpEntity))
                    {
                        using (var sqlTransaction = sqlConnection.BeginTransaction())
                        {
                            using (var cmd = sqlConnection.CreateCommand())
                            {
                                cmd.CommandText = ActionToSqlString('D');
                                cmd.CommandType = CommandType.Text;
                                cmd.Transaction = sqlTransaction;
                                DeleteCommandParameters(entity.Id, cmd);
                                i = cmd.ExecuteNonQuery();

                                sqlTransaction.Commit();
                            }
                        }
                    }
                    else
                    {
                        throw new ValidationException("dbo.Entity haven't this record of entity!", "");
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
        protected abstract void DeleteCommandParameters(int? id, SqlCommand cmd);
        protected abstract void GetByIdCommandParameters(int? id, SqlCommand cmd);
        protected abstract T Map(SqlDataReader reader);
        protected abstract List<T> Maps(SqlDataReader reader);
    }
}
