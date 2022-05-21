using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    public class EventRepository : BaseRepository<Event>, IRepository<Event>
    {
        public EventRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Passes the parameters for Insert Statement.
        /// </summary>
        /// <param name="entity">.</param>
        /// <param name="cmd">..</param>
        protected override void InsertCommandParameters(Event entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
        }

        /// <summary>
        /// Passes the parameters for Update Statement.
        /// </summary>
        /// <param name="entity">.</param>
        /// <param name="cmd">..</param>
        protected override void UpdateCommandParameters(Event entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
        }

        /// <summary>
        /// Passes the parameters to command for Delete Statement.
        /// </summary>
        /// <param name="id">.</param>
        /// <param name="cmd">..</param>
        protected override void DeleteCommandParameters(int id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        /// <summary>
        /// Passes the parameters to command for populate by key statement.
        /// </summary>
        /// <param name="id">.</param>
        /// <param name="cmd">..</param>
        protected override void GetByIdCommandParameters(int id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        /// <summary>
        /// Maps data for populate by key statement.
        /// </summary>
        /// <param name="reader">.</param>
        /// <returns>..</returns>
        protected override Event Map(SqlDataReader reader)
        {
            Event evnt = new Event();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    evnt.Id = Guid.Parse(reader["Id"].ToString());
                    evnt.Name = reader["Name"].ToString();
                    evnt.Description = reader["Description"].ToString();
                    evnt.LayoutId = Guid.Parse(reader["LayoutId"].ToString());
                }
            }

            return evnt;
        }

        /// <summary>
        /// Maps data for populate all statement.
        /// </summary>
        /// <param name="reader">.</param>
        /// <returns>..</returns>
        protected override List<Event> Maps(SqlDataReader reader)
        {
            List<Event> evnts = new List<Event>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Event evnt = new Event();
                    evnt.Id = Guid.Parse(reader["Id"].ToString());
                    evnt.Name = reader["Name"].ToString();
                    evnt.Description = reader["Description"].ToString();
                    evnt.LayoutId = Guid.Parse(reader["LayoutId"].ToString());
                    evnts.Add(evnt);
                }
            }

            return evnts;
        }
    }
}
