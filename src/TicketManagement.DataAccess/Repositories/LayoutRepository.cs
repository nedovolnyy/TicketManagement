using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.EF;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class LayoutRepository : BaseRepository<Layout>, ILayoutRepository
    {
        private readonly DatabaseContext _databaseContext;

        internal LayoutRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        protected override void AddParamsForInsert(Layout entity, DbCommand cmd)
        {
            cmd.CommandText = "INSERT INTO Layout (Name, VenueId, Description) VALUES (@Name, @VenueId, @Description);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)";
            cmd.AddWithValue("@Name", entity.Name);
            cmd.AddWithValue("@VenueId", entity.VenueId);
            cmd.AddWithValue("@Description", entity.Description);
        }

        protected override void AddParamsForUpdate(Layout entity, DbCommand cmd)
        {
            cmd.CommandText = "UPDATE Layout SET Name = @Name, VenueId = @VenueId, Description = @Description Where Id = @Id";
            cmd.AddWithValue("@Id", entity.Id);
            cmd.AddWithValue("@Name", entity.Name);
            cmd.AddWithValue("@VenueId", entity.VenueId);
            cmd.AddWithValue("@Description", entity.Description);
        }

        protected override void AddParamsForDelete(int id, DbCommand cmd)
        {
            cmd.CommandText = "DELETE FROM Layout WHERE Id = @Id";
            cmd.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, DbCommand cmd)
        {
            cmd.CommandText = "SELECT Id, Name, VenueId, Description FROM Layout WHERE Id = @Id";
            cmd.AddWithValue("@Id", id);
        }

        protected override void GetAllCommandParameters(DbCommand cmd)
        {
            cmd.CommandText = "SELECT Id, Name, VenueId, Description FROM Layout";
        }

        /// <summary>
        /// Method for populate data by id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="Layout"/>List&lt;Layout&gt;.</returns>
        public IEnumerable<Layout> GetAllByVenueId(int id)
        {
            var cmd = _databaseContext.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "SELECT Id, Name, VenueId, Description FROM Layout WHERE VenueId = @VenueId";
            cmd.CommandType = CommandType.Text;
            cmd.AddWithValue("@VenueId", id);
            using var reader = cmd.ExecuteReader();
            return Maps(reader);
        }

        protected override Layout Map(DbDataReader reader)
        {
            if (reader.HasRows)
            {
                reader.Read();
                return new Layout(id: int.Parse(reader["Id"].ToString()),
                                        name: reader["Name"].ToString(),
                                        venueId: int.Parse(reader["VenueId"].ToString()),
                                        description: reader["Description"].ToString());
            }

            return null;
        }

        protected override List<Layout> Maps(DbDataReader reader)
        {
            var areas = new List<Layout>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var layout = new Layout(id: int.Parse(reader["Id"].ToString()),
                                               name: reader["Name"].ToString(),
                                               venueId: int.Parse(reader["VenueId"].ToString()),
                                               description: reader["Description"].ToString());
                    areas.Add(layout);
                }
            }

            return areas;
        }
    }
}
