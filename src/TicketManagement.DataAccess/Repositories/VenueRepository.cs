using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    public class VenueRepository : BaseRepository<Venue>, IRepository<Venue>
    {
        public VenueRepository()
            : base()
        {
        }

        protected override string ActionToSqlString(char action) => action switch
        {
            'I' => "INSERT INTO Venue (Description, Address, Phone) VALUES (@Description, @Address, @Phone);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)",
            'U' => "UPDATE Venue SET Description = @Description, Address = @Address, Phone = @Phone Where Id = @Id",
            'D' => "DELETE FROM Venue WHERE Id = @Id",
            'G' => "SELECT * FROM Venue WHERE Id = @Id",
            'A' => "SELECT * FROM Venue",
            _ => ""
        };

        protected override void InsertCommandParameters(Venue entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@Address", entity.Address);
            cmd.Parameters.AddWithValue("@Phone", entity.Phone);
        }

        protected override void UpdateCommandParameters(Venue entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@Address", entity.Address);
            cmd.Parameters.AddWithValue("@Phone", entity.Phone);
        }

        protected override void DeleteCommandParameters(int id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetByIdCommandParameters(int id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override Venue Map(SqlDataReader reader)
        {
            Venue venue = new Venue();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    venue = new Venue(id: int.Parse(reader["Id"].ToString()),
                                      description: reader["Description"].ToString(),
                                      address: reader["Address"].ToString(),
                                      phone: reader["Phone"].ToString());
                }
            }

            return venue;
        }

        protected override List<Venue> Maps(SqlDataReader reader)
        {
            List<Venue> venues = new List<Venue>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Venue venue = new Venue(id: int.Parse(reader["Id"].ToString()),
                                      description: reader["Description"].ToString(),
                                      address: reader["Address"].ToString(),
                                      phone: reader["Phone"].ToString());
                    venues.Add(venue);
                }
            }

            return venues;
        }
    }
}
