using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.ADO;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventRepository : BaseRepository<Event>, IEventRepository
    {
        protected override string GetSQLStatement(string action) => "";
        private void ForStoredProcedure(SqlCommand cmd)
        {
            cmd.CommandText = "spEvent";
            cmd.CommandType = CommandType.StoredProcedure;
        }

        protected override void AddParamsForInsert(Event entity, SqlCommand cmd)
        {
            ForStoredProcedure(cmd);
            cmd.Parameters.AddWithValue("@Action", "Save");
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@EventTime", entity.EventTime);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
        }

        protected override void AddParamsForUpdate(Event entity, SqlCommand cmd)
        {
            ForStoredProcedure(cmd);
            cmd.Parameters.AddWithValue("@Action", "Save");
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@EventTime", entity.EventTime);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
        }

        protected override void AddParamsForDelete(int id, SqlCommand cmd)
        {
            ForStoredProcedure(cmd);
            cmd.Parameters.AddWithValue("@Action", "Delete");
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, SqlCommand cmd)
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
        /// Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="Event"/>List&lt;Event&gt;.</returns>
        public IEnumerable<Event> GetAllByLayoutId(int id)
        {
            using var sqlConnection = new DatabaseContext().Connection;
            using var cmd = sqlConnection.CreateCommand();
            ForStoredProcedure(cmd);
            cmd.Parameters.AddWithValue("@Action", "ForValidate");
            cmd.Parameters.AddWithValue("@LayoutId", id);
            using var reader = cmd.ExecuteReader();
            return Maps(reader);
        }

        protected override Event Map(SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                reader.Read();
                return new Event(id: int.Parse(reader["Id"].ToString()),
                                     name: reader["Name"].ToString(),
                                     eventTime: DateTimeOffset.Parse(reader["EventTime"].ToString()),
                                     description: reader["Description"].ToString(),
                                     layoutId: int.Parse(reader["LayoutId"].ToString()));
            }

            return null;
        }

        protected override List<Event> Maps(SqlDataReader reader)
        {
            var evnts = new List<Event>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var evnt = new Event(id: int.Parse(reader["Id"].ToString()),
                                     name: reader["Name"].ToString(),
                                     eventTime: DateTimeOffset.Parse(reader["EventTime"].ToString()),
                                     description: reader["Description"].ToString(),
                                     layoutId: int.Parse(reader["LayoutId"].ToString()));
                    evnts.Add(evnt);
                }
            }

            return evnts;
        }
    }
}
