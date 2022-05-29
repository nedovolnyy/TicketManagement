using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    public class LayoutRepository : BaseRepository<Layout>, IRepository<Layout>
    {
        public LayoutRepository()
            : base()
        {
        }

        protected override string ActionToSqlString(char action) => action switch
        {
            'I' => "INSERT INTO Layout (VenueId, Description) VALUES (@VenueId, @Description);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)",
            'U' => "UPDATE Layout SET VenueId = @VenueId, Description = @Description Where Id = @Id",
            'D' => "DELETE FROM Layout WHERE Id = @Id",
            'G' => "SELECT * FROM Layout WHERE Id = @Id",
            'A' => "SELECT * FROM Layout",
            _ => ""
        };

        protected override void InsertCommandParameters(Layout entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@VenueId", entity.VenueId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
        }

        protected override void UpdateCommandParameters(Layout entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@VenueId", entity.VenueId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
        }

        protected override void DeleteCommandParameters(int? id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetByIdCommandParameters(int? id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override Layout Map(SqlDataReader reader)
        {
            Layout layout = new Layout();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    layout = new Layout(id: int.Parse(reader["Id"].ToString()),
                                        venueId: int.Parse(reader["VenueId"].ToString()),
                                        description: reader["Description"].ToString());
                }
            }

            return layout;
        }

        protected override List<Layout> Maps(SqlDataReader reader)
        {
            List<Layout> areas = new List<Layout>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Layout layout = new Layout(id: int.Parse(reader["Id"].ToString()),
                                        venueId: int.Parse(reader["VenueId"].ToString()),
                                        description: reader["Description"].ToString());
                    areas.Add(layout);
                }
            }

            return areas;
        }
    }
}
