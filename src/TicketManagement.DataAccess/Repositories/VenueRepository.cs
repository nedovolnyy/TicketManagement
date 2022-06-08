using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.ADO;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class VenueRepository : BaseRepository<Venue>, IVenueRepository
    {
        protected override string GetSQLStatement(string action) => action switch
        {
            "Insert" => "INSERT INTO Venue (Name, Description, Address, Phone) VALUES (@Name, @Description, @Address, @Phone);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)",
            "Update" => "UPDATE Venue SET Name = @Name, Description = @Description, Address = @Address, Phone = @Phone Where Id = @Id",
            "Delete" => "DELETE FROM Venue WHERE Id = @Id",
            "GetById" => "SELECT Id, Name, Description, Address, Phone FROM Venue WHERE Id = @Id",
            "GetAll" => "SELECT Id, Name, Description, Address, Phone FROM Venue",
            "ActionForValidate" => "SELECT TOP 1 Id, Name, Description, Address, Phone FROM Venue WHERE Name = @Name",
            _ => ""
        };

        protected override void AddParamsForInsert(Venue entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@Address", entity.Address);
            cmd.Parameters.AddWithValue("@Phone", entity.Phone);
        }

        protected override void AddParamsForUpdate(Venue entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@Address", entity.Address);
            cmd.Parameters.AddWithValue("@Phone", entity.Phone);
        }

        protected override void AddParamsForDelete(int id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        /// <summary>
        /// Method for populate data by name.
        /// </summary>
        /// <param name="name">id.</param>
        /// <returns><see cref="Venue"/>List&lt;Seat&gt;.</returns>
        public Venue GetFirstByName(string name)
        {
            using var sqlConnection = new DatabaseContext().Connection;
            using var cmd = sqlConnection.CreateCommand();
            cmd.CommandText = GetSQLStatement("ActionForValidate");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Name", name);
            using var reader = cmd.ExecuteReader();
            return Map(reader);
        }

        protected override Venue Map(SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                reader.Read();
                return new Venue(id: int.Parse(reader["Id"].ToString()),
                                      name: reader["Name"].ToString(),
                                      description: reader["Description"].ToString(),
                                      address: reader["Address"].ToString(),
                                      phone: reader["Phone"].ToString());
            }

            return null;
        }

        protected override List<Venue> Maps(SqlDataReader reader)
        {
            List<Venue> venues = new List<Venue>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Venue venue = new Venue(id: int.Parse(reader["Id"].ToString()),
                                            name: reader["Name"].ToString(),
                                            description: reader["Description"].ToString(),
                                            address: reader["Address"].ToString(),
                                            phone: reader["Phone"].ToString());
                    venues.Add(venue);
                }
            }

            return venues;
        }

        protected override void GetAllCommandParameters(SqlCommand cmd)
        {
        }
    }
}
