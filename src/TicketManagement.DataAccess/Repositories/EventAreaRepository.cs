using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    public class EventAreaRepository : BaseRepository<EventArea>, IEventAreaRepository
    {
        public EventAreaRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Passes the parameters for Insert Statement.
        /// </summary>
        /// <param name="entity">.</param>
        /// <param name="cmd">..</param>
        protected override void InsertCommandParameters(EventArea entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@EventId", entity.EventId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@CoordX", entity.CoordX);
            cmd.Parameters.AddWithValue("@CoordY", entity.CoordY);
            cmd.Parameters.AddWithValue("@Price", entity.Price);
        }

        /// <summary>
        /// Passes the parameters for Update Statement.
        /// </summary>
        /// <param name="entity">.</param>
        /// <param name="cmd">..</param>
        protected override void UpdateCommandParameters(EventArea entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@EventId", entity.EventId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@CoordX", entity.CoordX);
            cmd.Parameters.AddWithValue("@CoordY", entity.CoordY);
            cmd.Parameters.AddWithValue("@Price", entity.Price);
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
        protected override EventArea Map(SqlDataReader reader)
        {
            EventArea eventArea = new EventArea();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    eventArea.Id = Convert.ToInt32(reader["Id"].ToString());
                    eventArea.EventId = Convert.ToInt32(reader["EventId"].ToString());
                    eventArea.Description = reader["Description"].ToString();
                    eventArea.CoordX = Convert.ToInt32(reader["CoordX"].ToString());
                    eventArea.CoordY = Convert.ToInt32(reader["CoordY"].ToString());
                    eventArea.Price = Convert.ToDecimal(reader["Price"].ToString());
                }
            }

            return eventArea;
        }

        /// <summary>
        /// Maps data for populate all statement.
        /// </summary>
        /// <param name="reader">.</param>
        /// <returns>..</returns>
        protected override List<EventArea> Maps(SqlDataReader reader)
        {
            List<EventArea> eventAreas = new List<EventArea>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EventArea eventArea = new EventArea();
                    eventArea.Id = Convert.ToInt32(reader["Id"].ToString());
                    eventArea.EventId = Convert.ToInt32(reader["EventId"].ToString());
                    eventArea.Description = reader["Description"].ToString();
                    eventArea.CoordX = Convert.ToInt32(reader["CoordX"].ToString());
                    eventArea.CoordY = Convert.ToInt32(reader["CoordY"].ToString());
                    eventArea.Price = Convert.ToDecimal(reader["Price"].ToString());
                    eventAreas.Add(eventArea);
                }
            }

            return eventAreas;
        }
    }
}
