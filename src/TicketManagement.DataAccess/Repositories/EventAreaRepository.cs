using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventAreaRepository : BaseRepository<EventArea>, IEventAreaRepository
    {
        private readonly IDatabaseContext _databaseContext;

        internal EventAreaRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        protected override void AddParamsForInsert(EventArea entity, SqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO EventArea (EventId, Description, CoordX, CoordY, Price) VALUES (@EventId, @Description, @CoordX, @CoordY, @Price);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)";
            cmd.Parameters.AddWithValue("@EventId", entity.EventId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@CoordX", entity.CoordX);
            cmd.Parameters.AddWithValue("@CoordY", entity.CoordY);
            cmd.Parameters.AddWithValue("@Price", entity.Price);
        }

        protected override void AddParamsForUpdate(EventArea entity, SqlCommand cmd)
        {
            cmd.CommandText = "UPDATE EventArea SET EventId = @EventId, Description = @Description, CoordX = @CoordX, CoordY = @CoordY, Price = @Price Where Id = @Id";
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@EventId", entity.EventId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@CoordX", entity.CoordX);
            cmd.Parameters.AddWithValue("@CoordY", entity.CoordY);
            cmd.Parameters.AddWithValue("@Price", entity.Price);
        }

        protected override void AddParamsForDelete(int id, SqlCommand cmd)
        {
            cmd.CommandText = "DELETE FROM EventArea WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, SqlCommand cmd)
        {
            cmd.CommandText = "SELECT Id, EventId, Description, CoordX, CoordY, Price FROM EventArea WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetAllCommandParameters(SqlCommand cmd)
        {
            cmd.CommandText = "SELECT Id, EventId, Description, CoordX, CoordY, Price FROM EventArea";
        }

        protected override EventArea Map(SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                reader.Read();
                return new EventArea(id: int.Parse(reader["Id"].ToString()),
                                              eventId: int.Parse(reader["EventId"].ToString()),
                                              description: reader["Description"].ToString(),
                                              coordX: int.Parse(reader["CoordX"].ToString()),
                                              coordY: int.Parse(reader["CoordY"].ToString()),
                                              price: decimal.Parse(reader["Price"].ToString()));
            }

            return null;
        }

        protected override List<EventArea> Maps(SqlDataReader reader)
        {
            var eventAreas = new List<EventArea>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var eventArea = new EventArea(id: int.Parse(reader["Id"].ToString()),
                                                        eventId: int.Parse(reader["EventId"].ToString()),
                                                        description: reader["Description"].ToString(),
                                                        coordX: int.Parse(reader["CoordX"].ToString()),
                                                        coordY: int.Parse(reader["CoordY"].ToString()),
                                                        price: decimal.Parse(reader["Price"].ToString()));
                    eventAreas.Add(eventArea);
                }
            }

            return eventAreas;
        }
    }
}
