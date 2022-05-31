using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.ADO;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal sealed class LayoutRepository : BaseRepository<Layout>, ILayoutRepository
    {
        protected override string ActionToSqlString(char action) => action switch
        {
            'I' => "INSERT INTO Layout (VenueId, Description) VALUES (@VenueId, @Description);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)",
            'U' => "UPDATE Layout SET VenueId = @VenueId, Description = @Description Where Id = @Id",
            'D' => "DELETE FROM Layout WHERE Id = @Id",
            'G' => "SELECT Id, VenueId, Description FROM Layout WHERE Id = @Id",
            'A' => "SELECT Id, VenueId, Description FROM Layout",
            'V' => "SELECT Id, VenueId, Description FROM Layout WHERE VenueId = @VenueId",
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

        /// <summary>
        /// Base Method for Populate Data by key.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>Get all Entity by VenueId.</returns>
        public IEnumerable<Layout> GetAllByVenueId(int? id)
        {
            try
            {
                using (SqlConnection sqlConnection = new DatabaseContext().Connection)
                {
                    using (var cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandText = ActionToSqlString('V');
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@VenueId", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            return Maps(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            else
            {
                throw new ValidationException("Don't have layouts to show!", "");
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
            else
            {
                throw new ValidationException("Don't have layouts to show!", "");
            }

            return areas;
        }
    }
}
