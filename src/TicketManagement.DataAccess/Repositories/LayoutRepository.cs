using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class LayoutRepository : BaseRepository<Layout>, ILayoutRepository
    {
        private readonly IDatabaseContext _databaseContext;

        internal LayoutRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        protected override void AddParamsForInsert(Layout entity, SqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO Layout (Name, VenueId, Description) VALUES (@Name, @VenueId, @Description);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)";
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@VenueId", entity.VenueId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
        }

        protected override void AddParamsForUpdate(Layout entity, SqlCommand cmd)
        {
            cmd.CommandText = "UPDATE Layout SET Name = @Name, VenueId = @VenueId, Description = @Description Where Id = @Id";
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@VenueId", entity.VenueId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
        }

        protected override void AddParamsForDelete(int id, SqlCommand cmd)
        {
            cmd.CommandText = "DELETE FROM Layout WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, SqlCommand cmd)
        {
            cmd.CommandText = "SELECT Id, Name, VenueId, Description FROM Layout WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetAllCommandParameters(SqlCommand cmd)
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
            var cmd = _databaseContext.Connection.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, VenueId, Description FROM Layout WHERE VenueId = @VenueId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@VenueId", id);
            using var reader = cmd.ExecuteReader();
            return Maps(reader);
        }

        protected override Layout Map(SqlDataReader reader)
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

        protected override List<Layout> Maps(SqlDataReader reader)
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
