using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class VenueRepository : BaseRepository<Venue>, IVenueRepository
    {
        private readonly IDatabaseContext _databaseContext;

        internal VenueRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        protected override void AddParamsForInsert(Venue entity, SqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO Venue (Name, Description, Address, Phone) VALUES (@Name, @Description, @Address, @Phone);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)";
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@Address", entity.Address);
            cmd.Parameters.AddWithValue("@Phone", entity.Phone);
        }

        protected override void AddParamsForUpdate(Venue entity, SqlCommand cmd)
        {
            cmd.CommandText = "UPDATE Venue SET Name = @Name, Description = @Description, Address = @Address, Phone = @Phone Where Id = @Id";
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@Address", entity.Address);
            cmd.Parameters.AddWithValue("@Phone", entity.Phone);
        }

        protected override void AddParamsForDelete(int id, SqlCommand cmd)
        {
            cmd.CommandText = "DELETE FROM Venue WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, SqlCommand cmd)
        {
            cmd.CommandText = "SELECT Id, Name, Description, Address, Phone FROM Venue WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetAllCommandParameters(SqlCommand cmd)
        {
            cmd.CommandText = "SELECT Id, Name, Description, Address, Phone FROM Venue";
        }

        /// <summary>
        /// Method for populate data by name.
        /// </summary>
        /// <param name="name">id.</param>
        /// <returns><see cref="int"/>First id, if in table Venue have same name.</returns>
        public int GetIdFirstByName(string name)
        {
            var cmd = _databaseContext.Connection.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 Id FROM Venue WHERE Name = @Name";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Name", name);
            var strId = cmd.ExecuteScalar();
            if (strId is null)
            {
                return default;
            }

            return int.Parse(strId.ToString());
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
            var venues = new List<Venue>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var venue = new Venue(id: int.Parse(reader["Id"].ToString()),
                                            name: reader["Name"].ToString(),
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
