using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
        private readonly IDatabaseContext _databaseContext;

        internal AreaRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        protected override void AddParamsForInsert(Area entity, SqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO Area (LayoutId, Description, CoordX, CoordY) VALUES (@LayoutId, @Description, @CoordX, @CoordY);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)";
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@CoordX", entity.CoordX);
            cmd.Parameters.AddWithValue("@CoordY", entity.CoordY);
        }

        protected override void AddParamsForUpdate(Area entity, SqlCommand cmd)
        {
            cmd.CommandText = "UPDATE Area SET LayoutId = @LayoutId, Description = @Description, CoordX = @CoordX, CoordY = @CoordY WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@CoordX", entity.CoordX);
            cmd.Parameters.AddWithValue("@CoordY", entity.CoordY);
        }

        protected override void AddParamsForDelete(int id, SqlCommand cmd)
        {
            cmd.CommandText = "DELETE FROM Area WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, SqlCommand cmd)
        {
            cmd.CommandText = "SELECT Id, LayoutId, Description, CoordX, CoordY FROM Area WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetAllCommandParameters(SqlCommand cmd)
        {
            cmd.CommandText = "SELECT Id, LayoutId, Description, CoordX, CoordY FROM Area";
        }

        /// <summary>
        /// Base Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="Area"/>List&lt;Area&gt;.</returns>
        IEnumerable<Area> IAreaRepository.GetAllByLayoutId(int id)
        {
            var cmd = _databaseContext.Connection.CreateCommand();
            cmd.CommandText = "SELECT Id, LayoutId, Description, CoordX, CoordY FROM Area WHERE LayoutId = @LayoutId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@LayoutId", id);
            using var reader = cmd.ExecuteReader();
            return Maps(reader);
        }

        protected override Area Map(SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                reader.Read();
                return new Area(id: int.Parse(reader["Id"].ToString()),
                                layoutId: int.Parse(reader["LayoutId"].ToString()),
                                description: reader["Description"].ToString(),
                                coordX: int.Parse(reader["CoordX"].ToString()),
                                coordY: int.Parse(reader["CoordY"].ToString()));
            }

            return null;
        }

        protected override List<Area> Maps(SqlDataReader reader)
        {
            var areas = new List<Area>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var area = new Area(id: int.Parse(reader["Id"].ToString()),
                                    layoutId: int.Parse(reader["LayoutId"].ToString()),
                                    description: reader["Description"].ToString(),
                                    coordX: int.Parse(reader["CoordX"].ToString()),
                                    coordY: int.Parse(reader["CoordY"].ToString()));
                    areas.Add(area);
                }
            }

            return areas;
        }
    }
}
