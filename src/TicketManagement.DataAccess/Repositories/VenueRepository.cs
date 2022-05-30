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
    public class VenueRepository : BaseRepository<Venue>, IVenueRepository
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
            'G' => "SELECT Id, Description, Address, Phone FROM Venue WHERE Id = @Id",
            'A' => "SELECT Id, Description, Address, Phone FROM Venue",
            'V' => "SELECT TOP 1 Id, Description, Address, Phone FROM Venue WHERE Description = @Description",
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
        /// <param name="name">name.</param>
        /// <returns>Get Venue by name.</returns>
        public Venue GetFirstByName(string name)
        {
            try
            {
                using (SqlConnection sqlConnection = new DatabaseContext().Connection)
                {
                    using (var cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandText = ActionToSqlString('V');
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Description", name);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            return Map(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            else
            {
                throw new ValidationException("Don't have venues to show!", "");
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
            else
            {
                throw new ValidationException("Don't have venues to show!", "");
            }

            return venues;
        }
    }
}
