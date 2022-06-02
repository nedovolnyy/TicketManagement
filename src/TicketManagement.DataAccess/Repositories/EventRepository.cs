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
    internal sealed class EventRepository : BaseRepository<Event>, IEventRepository
    {
        protected override string ActionToSqlString(char action) => "";
        private void ForStoredProcedure(SqlCommand cmd)
        {
            cmd.CommandText = "spEvent";
            cmd.CommandType = CommandType.StoredProcedure;
        }

        protected override void InsertCommandParameters(Event entity, SqlCommand cmd)
        {
            ForStoredProcedure(cmd);
            cmd.Parameters.AddWithValue("@Action", "Save");
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@EventTime", entity.EventTime);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
        }

        protected override void UpdateCommandParameters(Event entity, SqlCommand cmd)
        {
            ForStoredProcedure(cmd);
            cmd.Parameters.AddWithValue("@Action", "Save");
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@EventTime", entity.EventTime);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
        }

        protected override void DeleteCommandParameters(int? id, SqlCommand cmd)
        {
            ForStoredProcedure(cmd);
            cmd.Parameters.AddWithValue("@Action", "Delete");
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetByIdCommandParameters(int? id, SqlCommand cmd)
        {
            ForStoredProcedure(cmd);
            cmd.Parameters.AddWithValue("@Action", "GetById");
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetAllCommandParameters(SqlCommand cmd)
        {
            ForStoredProcedure(cmd);
            cmd.Parameters.AddWithValue("@Action", "GetAll");
        }

        /// <summary>
        /// Base Method for Populate Data by key.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>Get all Entity by LayoutId.</returns>
        public IEnumerable<Event> GetAllByLayoutId(int? id)
        {
            try
            {
                using (SqlConnection sqlConnection = new DatabaseContext().Connection)
                {
                    using (var cmd = sqlConnection.CreateCommand())
                    {
                        ForStoredProcedure(cmd);
                        cmd.Parameters.AddWithValue("@Action", "ForValidate");
                        cmd.Parameters.AddWithValue("@LayoutId", id);
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

        protected override Event Map(SqlDataReader reader)
        {
            Event evnt = new Event();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    evnt = new Event(id: int.Parse(reader["Id"].ToString()),
                                     name: reader["Name"].ToString(),
                                     eventTime: DateTimeOffset.Parse(reader["EventTime"].ToString()),
                                     description: reader["Description"].ToString(),
                                     layoutId: int.Parse(reader["LayoutId"].ToString()));
                }
            }
            else
            {
                throw new ValidationException("Don't have events to show!", "");
            }

            return evnt;
        }

        protected override List<Event> Maps(SqlDataReader reader)
        {
            List<Event> evnts = new List<Event>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Event evnt = new Event(id: int.Parse(reader["Id"].ToString()),
                                     name: reader["Name"].ToString(),
                                     eventTime: DateTimeOffset.Parse(reader["EventTime"].ToString()),
                                     description: reader["Description"].ToString(),
                                     layoutId: int.Parse(reader["LayoutId"].ToString()));
                    evnts.Add(evnt);
                }
            }
            else
            {
                throw new ValidationException("Don't have events to show!", "");
            }

            return evnts;
        }
    }
}
