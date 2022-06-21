using System.Collections.Generic;
using System.Data.Common;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.EF;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventAreaRepository : BaseRepository<EventArea>, IEventAreaRepository
    {
        private readonly DatabaseContext _databaseContext;

        internal EventAreaRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        protected override void AddParamsForInsert(EventArea entity, DbCommand cmd)
        {
            cmd.CommandText = "INSERT INTO EventArea (EventId, Description, CoordX, CoordY, Price) VALUES (@EventId, @Description, @CoordX, @CoordY, @Price);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)";
            cmd.AddWithValue("@EventId", entity.EventId);
            cmd.AddWithValue("@Description", entity.Description);
            cmd.AddWithValue("@CoordX", entity.CoordX);
            cmd.AddWithValue("@CoordY", entity.CoordY);
            cmd.AddWithValue("@Price", entity.Price);
        }

        protected override void AddParamsForUpdate(EventArea entity, DbCommand cmd)
        {
            cmd.CommandText = "UPDATE EventArea SET EventId = @EventId, Description = @Description, CoordX = @CoordX, CoordY = @CoordY, Price = @Price Where Id = @Id";
            cmd.AddWithValue("@Id", entity.Id);
            cmd.AddWithValue("@EventId", entity.EventId);
            cmd.AddWithValue("@Description", entity.Description);
            cmd.AddWithValue("@CoordX", entity.CoordX);
            cmd.AddWithValue("@CoordY", entity.CoordY);
            cmd.AddWithValue("@Price", entity.Price);
        }

        protected override void AddParamsForDelete(int id, DbCommand cmd)
        {
            cmd.CommandText = "DELETE FROM EventArea WHERE Id = @Id";
            cmd.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, DbCommand cmd)
        {
            cmd.CommandText = "SELECT Id, EventId, Description, CoordX, CoordY, Price FROM EventArea WHERE Id = @Id";
            cmd.AddWithValue("@Id", id);
        }

        protected override void GetAllCommandParameters(DbCommand cmd)
        {
            cmd.CommandText = "SELECT Id, EventId, Description, CoordX, CoordY, Price FROM EventArea";
        }

        protected override EventArea Map(DbDataReader reader)
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

        protected override List<EventArea> Maps(DbDataReader reader)
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
