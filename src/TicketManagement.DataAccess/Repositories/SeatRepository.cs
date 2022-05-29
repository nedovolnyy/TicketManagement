using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    public class SeatRepository : BaseRepository<Seat>, IRepository<Seat>
    {
        public SeatRepository()
            : base()
        {
        }

        protected override string ActionToSqlString(char action) => action switch
        {
            'I' => "INSERT INTO Seat (AreaId, Row, Number) VALUES (@AreaId, @Row, @Number);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)",
            'U' => "UPDATE Seat SET AreaId = @AreaId, Row = @Row, Number = @Number Where Id = @Id",
            'D' => "DELETE FROM Seat WHERE Id = @Id",
            'G' => "SELECT * FROM Seat WHERE Id = @Id",
            'A' => "SELECT * FROM Seat",
            _ => ""
        };

        protected override void InsertCommandParameters(Seat entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@AreaId", entity.AreaId);
            cmd.Parameters.AddWithValue("@Row", entity.Row);
            cmd.Parameters.AddWithValue("@Number", entity.Number);
        }

        protected override void UpdateCommandParameters(Seat entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@AreaId", entity.AreaId);
            cmd.Parameters.AddWithValue("@Row", entity.Row);
            cmd.Parameters.AddWithValue("@Number", entity.Number);
        }

        protected override void DeleteCommandParameters(int? id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetByIdCommandParameters(int? id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override Seat Map(SqlDataReader reader)
        {
            Seat seat = new Seat();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    seat = new Seat(id: int.Parse(reader["Id"].ToString()),
                                    areaId: int.Parse(reader["AreaId"].ToString()),
                                    row: Convert.ToInt32(reader["Row"].ToString()),
                                    number: Convert.ToInt32(reader["Number"].ToString()));
                }
            }

            return seat;
        }

        protected override List<Seat> Maps(SqlDataReader reader)
        {
            List<Seat> seats = new List<Seat>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Seat seat = new Seat(id: int.Parse(reader["Id"].ToString()),
                                    areaId: int.Parse(reader["AreaId"].ToString()),
                                    row: Convert.ToInt32(reader["Row"].ToString()),
                                    number: Convert.ToInt32(reader["Number"].ToString()));
                    seats.Add(seat);
                }
            }

            return seats;
        }
    }
}
