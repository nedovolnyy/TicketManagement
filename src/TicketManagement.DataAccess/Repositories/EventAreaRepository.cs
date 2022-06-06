using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventAreaRepository : BaseRepository<EventArea>, IEventAreaRepository
    {
        protected override string GetSQLStatement(string action) => action switch
        {
            "Insert" => "INSERT INTO EventArea (EventId, Description, CoordX, CoordY, Price) VALUES (@EventId, @Description, @CoordX, @CoordY, @Price);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)",
            "Update" => "UPDATE EventArea SET EventId = @EventId, Description = @Description, CoordX = @CoordX, CoordY = @CoordY, Price = @Price Where Id = @Id",
            "Delete" => "DELETE FROM EventArea WHERE Id = @Id",
            "GetById" => "SELECT Id, EventId, Description, CoordX, CoordY, Price FROM EventArea WHERE Id = @Id",
            "GetAll" => "SELECT Id, EventId, Description, CoordX, CoordY, Price FROM EventArea",
            _ => ""
        };

        protected override void AddParamsForInsert(EventArea entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@EventId", entity.EventId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@CoordX", entity.CoordX);
            cmd.Parameters.AddWithValue("@CoordY", entity.CoordY);
            cmd.Parameters.AddWithValue("@Price", entity.Price);
        }

        protected override void AddParamsForUpdate(EventArea entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@EventId", entity.EventId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@CoordX", entity.CoordX);
            cmd.Parameters.AddWithValue("@CoordY", entity.CoordY);
            cmd.Parameters.AddWithValue("@Price", entity.Price);
        }

        protected override void AddParamsForDelete(int id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override EventArea Map(SqlDataReader reader)
        {
            EventArea eventArea = new EventArea();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    eventArea = new EventArea(id: int.Parse(reader["Id"].ToString()),
                                              eventId: int.Parse(reader["EventId"].ToString()),
                                              description: reader["Description"].ToString(),
                                              coordX: int.Parse(reader["CoordX"].ToString()),
                                              coordY: int.Parse(reader["CoordY"].ToString()),
                                              price: decimal.Parse(reader["Price"].ToString()));
                }
            }
            else
            {
                throw new ValidationException("Don't have eventAreas to show!", "");
            }

            return eventArea;
        }

        protected override List<EventArea> Maps(SqlDataReader reader)
        {
            List<EventArea> eventAreas = new List<EventArea>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EventArea eventArea = new EventArea(id: int.Parse(reader["Id"].ToString()),
                                                        eventId: int.Parse(reader["EventId"].ToString()),
                                                        description: reader["Description"].ToString(),
                                                        coordX: int.Parse(reader["CoordX"].ToString()),
                                                        coordY: int.Parse(reader["CoordY"].ToString()),
                                                        price: decimal.Parse(reader["Price"].ToString()));
                    eventAreas.Add(eventArea);
                }
            }
            else
            {
                throw new ValidationException("Don't have eventAreas to show!", "");
            }

            return eventAreas;
        }

        protected override void GetAllCommandParameters(SqlCommand cmd)
        {
        }
    }
}
