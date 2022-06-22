using System.Collections.Generic;
using System.Data.Common;
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

        protected override void AddParamsForInsert(Seat entity, DbCommand cmd)
        {
            cmd.CommandText = "INSERT INTO Seat (AreaId, Row, Number) VALUES (@AreaId, @Row, @Number);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)";
            cmd.AddWithValue("@AreaId", entity.AreaId);
            cmd.AddWithValue("@Row", entity.Row);
            cmd.AddWithValue("@Number", entity.Number);
        }

        protected override void AddParamsForUpdate(Seat entity, DbCommand cmd)
        {
            cmd.CommandText = "UPDATE Seat SET AreaId = @AreaId, Row = @Row, Number = @Number Where Id = @Id";
            cmd.AddWithValue("@Id", entity.Id);
            cmd.AddWithValue("@AreaId", entity.AreaId);
            cmd.AddWithValue("@Row", entity.Row);
            cmd.AddWithValue("@Number", entity.Number);
        }

        protected override void AddParamsForDelete(int id, DbCommand cmd)
        {
            cmd.CommandText = "DELETE FROM Seat WHERE Id = @Id";
            cmd.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, DbCommand cmd)
        {
            cmd.CommandText = "SELECT Id, AreaId, Row, Number FROM Seat WHERE Id = @Id";
            cmd.AddWithValue("@Id", id);
        }

        protected override void GetAllCommandParameters(DbCommand cmd)
        {
            cmd.CommandText = "SELECT Id, AreaId, Row, Number FROM Seat";
        }

        public IEnumerable<Seat> GetAllByAreaId(int id)
        {
            var cmd = _databaseContext.Connection.CreateCommand();
            cmd.CommandText = "SELECT Id, AreaId, Row, Number FROM Seat WHERE AreaId = @AreaId";
            cmd.AddWithValue("@AreaId", id);
            using var reader = cmd.ExecuteReader();
            return Maps(reader);
        }

        protected override Seat Map(DbDataReader reader)
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

        protected override List<Seat> Maps(DbDataReader reader)
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
