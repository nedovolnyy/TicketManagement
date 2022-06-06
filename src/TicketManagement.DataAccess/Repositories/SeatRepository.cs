using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.ADO;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class SeatRepository : BaseRepository<Seat>, ISeatRepository
    {
        protected override string GetSQLStatement(string action) => action switch
        {
            "Insert" => "INSERT INTO Seat (AreaId, Row, Number) VALUES (@AreaId, @Row, @Number);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)",
            "Update" => "UPDATE Seat SET AreaId = @AreaId, Row = @Row, Number = @Number Where Id = @Id",
            "Delete" => "DELETE FROM Seat WHERE Id = @Id",
            "GetById" => "SELECT Id, AreaId, Row, Number FROM Seat WHERE Id = @Id",
            "GetAll" => "SELECT Id, AreaId, Row, Number FROM Seat",
            "ActionForValidate" => "SELECT Id, AreaId, Row, Number FROM Seat WHERE AreaId = @AreaId",
            _ => ""
        };

        protected override void AddParamsForInsert(Seat entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@AreaId", entity.AreaId);
            cmd.Parameters.AddWithValue("@Row", entity.Row);
            cmd.Parameters.AddWithValue("@Number", entity.Number);
        }

        protected override void AddParamsForUpdate(Seat entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@AreaId", entity.AreaId);
            cmd.Parameters.AddWithValue("@Row", entity.Row);
            cmd.Parameters.AddWithValue("@Number", entity.Number);
        }

        protected override void AddParamsForDelete(int id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        /// <summary>
        /// Base Method for Populate Data by key.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>Get all Entity by AreaId.</returns>
        public IEnumerable<Seat> GetAllByAreaId(int id)
        {
            using (var cmd = new DatabaseContext().Connection.CreateCommand())
            {
                cmd.CommandText = GetSQLStatement("ActionForValidate");
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@AreaId", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return Maps(reader);
                }
            }
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
                                    row: int.Parse(reader["Row"].ToString()),
                                    number: int.Parse(reader["Number"].ToString()));
                }
            }
            else
            {
                throw new ValidationException("Don't have seats to show!", "");
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
                                    row: int.Parse(reader["Row"].ToString()),
                                    number: int.Parse(reader["Number"].ToString()));
                    seats.Add(seat);
                }
            }
            else
            {
                throw new ValidationException("Don't have seats to show!", "");
            }

            return seats;
        }

        protected override void GetAllCommandParameters(SqlCommand cmd)
        {
        }
    }
}
