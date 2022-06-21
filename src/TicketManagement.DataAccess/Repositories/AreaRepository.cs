using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.EF;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
        private readonly DatabaseContext _databaseContext;

        internal AreaRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _databaseContext.Database.OpenConnection();
        }

        protected override void AddParamsForInsert(Area entity, DbCommand cmd)
        {
            cmd.CommandText = "INSERT INTO Area (LayoutId, Description, CoordX, CoordY) VALUES (@LayoutId, @Description, @CoordX, @CoordY);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)";
            cmd.AddWithValue("@LayoutId", entity.LayoutId);
            cmd.AddWithValue("@Description", entity.Description);
            cmd.AddWithValue("@CoordX", entity.CoordX);
            cmd.AddWithValue("@CoordY", entity.CoordY);
        }

        protected override void AddParamsForUpdate(Area entity, DbCommand cmd)
        {
            cmd.CommandText = "UPDATE Area SET LayoutId = @LayoutId, Description = @Description, CoordX = @CoordX, CoordY = @CoordY WHERE Id = @Id";
            cmd.AddWithValue("@Id", entity.Id);
            cmd.AddWithValue("@LayoutId", entity.LayoutId);
            cmd.AddWithValue("@Description", entity.Description);
            cmd.AddWithValue("@CoordX", entity.CoordX);
            cmd.AddWithValue("@CoordY", entity.CoordY);
        }

        protected override void AddParamsForDelete(int id, DbCommand cmd)
        {
            cmd.CommandText = "DELETE FROM Area WHERE Id = @Id";
            cmd.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, DbCommand cmd)
        {
            cmd.CommandText = "SELECT Id, LayoutId, Description, CoordX, CoordY FROM Area WHERE Id = @Id";
            cmd.AddWithValue("@Id", id);
        }

        protected override void GetAllCommandParameters(DbCommand cmd)
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
            var cmd = _databaseContext.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "SELECT Id, LayoutId, Description, CoordX, CoordY FROM Area WHERE LayoutId = @LayoutId";
            cmd.AddWithValue("@LayoutId", id);
            using var reader = cmd.ExecuteReader();
            return Maps(reader);
        }

        protected override Area Map(DbDataReader reader)
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

        protected override List<Area> Maps(DbDataReader reader)
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
