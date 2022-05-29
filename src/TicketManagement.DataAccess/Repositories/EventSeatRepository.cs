using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    public class EventSeatRepository : BaseRepository<EventSeat>, IEventSeatRepository
    {
        public EventSeatRepository()
            : base()
        {
        }

        protected override string ActionToSqlString(char action) => action switch
        {
            'I' => "INSERT INTO EventSeat (EventAreaId, Row, Number, State) VALUES (@EventAreaId, @Row, @Number, @State);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)",
            'U' => "UPDATE EventSeat SET EventAreaId = @EventAreaId, Row = @Row, Number = @Number, State = @State Where Id = @Id",
            'D' => "DELETE FROM EventSeat WHERE Id = @Id",
            'G' => "SELECT * FROM EventSeat WHERE Id = @Id",
            'A' => "SELECT * FROM EventSeat",
            _ => ""
        };

        protected override void InsertCommandParameters(EventSeat entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@EventAreaId", entity.EventAreaId);
            cmd.Parameters.AddWithValue("@Row", entity.Row);
            cmd.Parameters.AddWithValue("@Number", entity.Number);
            cmd.Parameters.AddWithValue("@State", entity.State);
        }

        protected override void UpdateCommandParameters(EventSeat entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@EventAreaId", entity.EventAreaId);
            cmd.Parameters.AddWithValue("@Row", entity.Row);
            cmd.Parameters.AddWithValue("@Number", entity.Number);
            cmd.Parameters.AddWithValue("@State", entity.State);
        }

        protected override void DeleteCommandParameters(int? id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetByIdCommandParameters(int? id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override EventSeat Map(SqlDataReader reader)
        {
            EventSeat eventSeat = new EventSeat();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    eventSeat = new EventSeat(id: int.Parse(reader["Id"].ToString()),
                                              eventAreaId: int.Parse(reader["EventId"].ToString()),
                                              row: Convert.ToInt32(reader["Row"].ToString()),
                                              number: Convert.ToInt32(reader["Number"].ToString()),
                                              state: Convert.ToInt32(reader["State"].ToString()));
                }
            }

            return eventSeat;
        }

        protected override List<EventSeat> Maps(SqlDataReader reader)
        {
            List<EventSeat> eventSeats = new List<EventSeat>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EventSeat eventSeat = new EventSeat(id: int.Parse(reader["Id"].ToString()),
                                              eventAreaId: int.Parse(reader["EventId"].ToString()),
                                              row: Convert.ToInt32(reader["Row"].ToString()),
                                              number: Convert.ToInt32(reader["Number"].ToString()),
                                              state: Convert.ToInt32(reader["State"].ToString()));
                    eventSeats.Add(eventSeat);
                }
            }

            return eventSeats;
        }
    }
}
