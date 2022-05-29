﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.ADO;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository()
            : base()
        {
        }

        protected override string ActionToSqlString(char action) => action switch
        {
            'I' => "INSERT INTO Event (Name, Description, LayoutId) VALUES (@Name, @Description, @LayoutId);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)",
            'U' => "UPDATE Event SET Name = @Name, Description = @Description, LayoutId = @LayoutId Where Id = @Id",
            'D' => "DELETE FROM Event WHERE Id = @Id",
            'G' => "SELECT * FROM Event WHERE Id = @Id",
            'A' => "SELECT * FROM Event",
            'V' => "SELECT * FROM Event WHERE LayoutId = @LayoutId",
            _ => ""
        };

        protected override void InsertCommandParameters(Event entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
        }

        protected override void UpdateCommandParameters(Event entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
        }

        protected override void DeleteCommandParameters(int? id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetByIdCommandParameters(int? id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
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
                        cmd.CommandText = ActionToSqlString('V');
                        cmd.CommandType = CommandType.Text;
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
                                     description: reader["Description"].ToString(),
                                     layoutId: int.Parse(reader["LayoutId"].ToString()));
                }
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
                                     description: reader["Description"].ToString(),
                                     layoutId: int.Parse(reader["LayoutId"].ToString()));
                    evnts.Add(evnt);
                }
            }

            return evnts;
        }
    }
}
