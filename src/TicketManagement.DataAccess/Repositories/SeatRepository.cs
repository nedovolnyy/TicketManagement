using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class SeatRepository : BaseRepository<Seat>, ISeatRepository
    {
        private readonly IDatabaseContext _databaseContext;

        internal SeatRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        protected override void AddParamsForInsert(Seat entity, SqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO Seat (AreaId, Row, Number) VALUES (@AreaId, @Row, @Number);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)";
            cmd.Parameters.AddWithValue("@AreaId", entity.AreaId);
            cmd.Parameters.AddWithValue("@Row", entity.Row);
            cmd.Parameters.AddWithValue("@Number", entity.Number);
        }

        protected override void AddParamsForUpdate(Seat entity, SqlCommand cmd)
        {
            cmd.CommandText = "UPDATE Seat SET AreaId = @AreaId, Row = @Row, Number = @Number Where Id = @Id";
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@AreaId", entity.AreaId);
            cmd.Parameters.AddWithValue("@Row", entity.Row);
            cmd.Parameters.AddWithValue("@Number", entity.Number);
        }

        protected override void AddParamsForDelete(int id, SqlCommand cmd)
        {
            cmd.CommandText = "DELETE FROM Seat WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, SqlCommand cmd)
        {
            cmd.CommandText = "SELECT Id, AreaId, Row, Number FROM Seat WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetAllCommandParameters(SqlCommand cmd)
        {
            cmd.CommandText = "SELECT Id, AreaId, Row, Number FROM Seat";
        }

        /// <summary>
        /// Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="Seat"/>List&lt;Seat&gt;.</returns>
        public IEnumerable<Seat> GetAllByAreaId(int id)
        {
            var cmd = _databaseContext.Connection.CreateCommand();
            cmd.CommandText = "SELECT Id, AreaId, Row, Number FROM Seat WHERE AreaId = @AreaId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@AreaId", id);
            using var reader = cmd.ExecuteReader();
            return Maps(reader);
        }

        protected override Seat Map(SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                reader.Read();
                return new Seat(id: int.Parse(reader["Id"].ToString()),
                                    areaId: int.Parse(reader["AreaId"].ToString()),
                                    row: int.Parse(reader["Row"].ToString()),
                                    number: int.Parse(reader["Number"].ToString()));
            }

            return null;
        }

        protected override List<Seat> Maps(SqlDataReader reader)
        {
            var seats = new List<Seat>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var seat = new Seat(id: int.Parse(reader["Id"].ToString()),
                                    areaId: int.Parse(reader["AreaId"].ToString()),
                                    row: int.Parse(reader["Row"].ToString()),
                                    number: int.Parse(reader["Number"].ToString()));
                    seats.Add(seat);
                }
            }

            return seats;
        }
    }
}
