using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    public class EventSeatRepository : BaseRepository<EventSeat>, IRepository<EventSeat>
    {
        public EventSeatRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Passes the parameters for Insert Statement.
        /// </summary>
        /// <param name="entity">.</param>
        /// <param name="cmd">..</param>
        protected override void InsertCommandParameters(EventSeat entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@EventAreaId", entity.EventAreaId);
            cmd.Parameters.AddWithValue("@Row", entity.Row);
            cmd.Parameters.AddWithValue("@Number", entity.Number);
            cmd.Parameters.AddWithValue("@State", entity.State);
        }

        /// <summary>
        /// Passes the parameters for Update Statement.
        /// </summary>
        /// <param name="entity">.</param>
        /// <param name="cmd">..</param>
        protected override void UpdateCommandParameters(EventSeat entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@EventAreaId", entity.EventAreaId);
            cmd.Parameters.AddWithValue("@Row", entity.Row);
            cmd.Parameters.AddWithValue("@Number", entity.Number);
            cmd.Parameters.AddWithValue("@State", entity.State);
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
        protected override EventSeat Map(SqlDataReader reader)
        {
            EventSeat eventSeat = new EventSeat();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    eventSeat.Id = Guid.Parse(reader["Id"].ToString());
                    eventSeat.EventAreaId = Guid.Parse(reader["EventAreaId"].ToString());
                    eventSeat.Row = Convert.ToInt32(reader["Row"].ToString());
                    eventSeat.Number = Convert.ToInt32(reader["Number"].ToString());
                    eventSeat.State = Convert.ToInt32(reader["State"].ToString());
                }
            }

            return eventSeat;
        }

        /// <summary>
        /// Maps data for populate all statement.
        /// </summary>
        /// <param name="reader">.</param>
        /// <returns>..</returns>
        protected override List<EventSeat> Maps(SqlDataReader reader)
        {
            List<EventSeat> eventSeats = new List<EventSeat>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EventSeat eventSeat = new EventSeat();
                    eventSeat.Id = Guid.Parse(reader["Id"].ToString());
                    eventSeat.EventAreaId = Guid.Parse(reader["EventAreaId"].ToString());
                    eventSeat.Row = Convert.ToInt32(reader["Row"].ToString());
                    eventSeat.Number = Convert.ToInt32(reader["Number"].ToString());
                    eventSeat.State = Convert.ToInt32(reader["State"].ToString());
                    eventSeats.Add(eventSeat);
                }
            }

            return eventSeats;
        }
    }
}
