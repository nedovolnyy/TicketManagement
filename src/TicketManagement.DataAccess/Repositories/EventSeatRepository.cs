using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventSeatRepository : BaseRepository<EventSeat>, IEventSeatRepository
    {
        private readonly IDatabaseContext _databaseContext;

        internal EventSeatRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        protected override void AddParamsForInsert(EventSeat entity, SqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO EventSeat (EventAreaId, Row, Number, State) VALUES (@EventAreaId, @Row, @Number, @State);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)";
            cmd.Parameters.AddWithValue("@EventAreaId", entity.EventAreaId);
            cmd.Parameters.AddWithValue("@Row", entity.Row);
            cmd.Parameters.AddWithValue("@Number", entity.Number);
            cmd.Parameters.AddWithValue("@State", entity.State);
        }

        protected override void AddParamsForUpdate(EventSeat entity, SqlCommand cmd)
        {
            cmd.CommandText = "UPDATE EventSeat SET EventAreaId = @EventAreaId, Row = @Row, Number = @Number, State = @State Where Id = @Id";
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@EventAreaId", entity.EventAreaId);
            cmd.Parameters.AddWithValue("@Row", entity.Row);
            cmd.Parameters.AddWithValue("@Number", entity.Number);
            cmd.Parameters.AddWithValue("@State", entity.State);
        }

        protected override void AddParamsForDelete(int id, SqlCommand cmd)
        {
            cmd.CommandText = "DELETE FROM EventSeat WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, SqlCommand cmd)
        {
            cmd.CommandText = "SELECT Id, EventAreaId, Row, Number, State FROM EventSeat WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetAllCommandParameters(SqlCommand cmd)
        {
            cmd.CommandText = "SELECT Id, EventAreaId, Row, Number, State FROM EventSeat";
        }

        protected override EventSeat Map(SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                reader.Read();
                return new EventSeat(id: int.Parse(reader["Id"].ToString()),
                                              eventAreaId: int.Parse(reader["EventAreaId"].ToString()),
                                              row: int.Parse(reader["Row"].ToString()),
                                              number: int.Parse(reader["Number"].ToString()),
                                              state: int.Parse(reader["State"].ToString()));
            }

            return null;
        }

        protected override List<EventSeat> Maps(SqlDataReader reader)
        {
            var eventSeats = new List<EventSeat>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var eventSeat = new EventSeat(id: int.Parse(reader["Id"].ToString()),
                                              eventAreaId: int.Parse(reader["EventAreaId"].ToString()),
                                              row: int.Parse(reader["Row"].ToString()),
                                              number: int.Parse(reader["Number"].ToString()),
                                              state: int.Parse(reader["State"].ToString()));
                    eventSeats.Add(eventSeat);
                }
            }

            return eventSeats;
        }
    }
}
