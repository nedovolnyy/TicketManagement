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
    public class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
        public AreaRepository()
            : base()
        {
        }

        protected override string ActionToSqlString(char action) => action switch
        {
            'I' => "INSERT INTO Area (LayoutId, Description, CoordX, CoordY) VALUES (@LayoutId, @Description, @CoordX, @CoordY);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)",
            'U' => "UPDATE Area SET LayoutId = @LayoutId, Description = @Description, CoordX = @CoordX, CoordY = @CoordY WHERE Id = @Id",
            'D' => "DELETE FROM Area WHERE Id = @Id",
            'G' => "SELECT Id, LayoutId, Description, CoordX, CoordY FROM Area WHERE Id = @Id",
            'A' => "SELECT Id, LayoutId, Description, CoordX, CoordY FROM Area",
            'V' => "SELECT Id, LayoutId, Description, CoordX, CoordY FROM Area WHERE LayoutId = @LayoutId",
            _ => ""
        };

        protected override void InsertCommandParameters(Area entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@CoordX", entity.CoordX);
            cmd.Parameters.AddWithValue("@CoordY", entity.CoordY);
        }

        protected override void UpdateCommandParameters(Area entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@CoordX", entity.CoordX);
            cmd.Parameters.AddWithValue("@CoordY", entity.CoordY);
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
        /// <returns>Get all Entity by LayoutId.</returns>
        public IEnumerable<Area> GetAllByLayoutId(int? id)
        {
            try
            {
                using (SqlConnection sqlConnection = new DatabaseContext().Connection)
                {
                    using (var cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandText = ActionToSqlString('V');
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@LayoutId", id);
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

        protected override Area Map(SqlDataReader reader)
        {
            Area area = new Area();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    area = new Area(id: int.Parse(reader["Id"].ToString()),
                                    layoutId: int.Parse(reader["LayoutId"].ToString()),
                                    description: reader["Description"].ToString(),
                                    coordX: Convert.ToInt32(reader["CoordX"].ToString()),
                                    coordY: Convert.ToInt32(reader["CoordY"].ToString()));
                }
            }
            else
            {
                throw new ValidationException("Don't have areas to show!", "");
            }

            return area;
        }

        protected override List<Area> Maps(SqlDataReader reader)
        {
            List<Area> areas = new List<Area>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Area area = new Area(id: int.Parse(reader["Id"].ToString()),
                                    layoutId: int.Parse(reader["LayoutId"].ToString()),
                                    description: reader["Description"].ToString(),
                                    coordX: Convert.ToInt32(reader["CoordX"].ToString()),
                                    coordY: Convert.ToInt32(reader["CoordY"].ToString()));
                    areas.Add(area);
                }
            }
            else
            {
                throw new ValidationException("Don't have areas to show!", "");
            }

            return areas;
        }
    }
}
