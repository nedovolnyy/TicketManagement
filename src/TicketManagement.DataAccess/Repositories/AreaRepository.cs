using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    public class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
        public AreaRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Passes the parameters for Insert Statement.
        /// </summary>
        /// <param name="entity">.</param>
        /// <param name="cmd">..</param>
        protected override void InsertCommandParameters(Area entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@CoordX", entity.CoordX);
            cmd.Parameters.AddWithValue("@CoordY", entity.CoordY);
        }

        /// <summary>
        /// Passes the parameters for Update Statement.
        /// </summary>
        /// <param name="entity">.</param>
        /// <param name="cmd">..</param>
        protected override void UpdateCommandParameters(Area entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@CoordX", entity.CoordX);
            cmd.Parameters.AddWithValue("@CoordY", entity.CoordY);
        }

        /// <summary>
        /// Passes the parameters to command for Delete Statement.
        /// </summary>
        /// <param name="id">.</param>
        /// <param name="cmd">..</param>
        protected override void DeleteCommandParameters(int id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        /// <summary>
        /// Passes the parameters to command for populate by key statement.
        /// </summary>
        /// <param name="id">.</param>
        /// <param name="cmd">..</param>
        protected override void GetByIdCommandParameters(int id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        /// <summary>
        /// Maps data for populate by key statement.
        /// </summary>
        /// <param name="reader">.</param>
        /// <returns>..</returns>
        protected override Area Map(SqlDataReader reader)
        {
            Area area = new Area();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    area.Id = Convert.ToInt32(reader["Id"].ToString());
                    area.LayoutId = Convert.ToInt32(reader["LayoutId"].ToString());
                    area.Description = reader["Description"].ToString();
                    area.CoordX = Convert.ToInt32(reader["CoordX"].ToString());
                    area.CoordY = Convert.ToInt32(reader["CoordY"].ToString());
                }
            }

            return area;
        }

        /// <summary>
        /// Maps data for populate all statement.
        /// </summary>
        /// <param name="reader">.</param>
        /// <returns>..</returns>
        protected override List<Area> Maps(SqlDataReader reader)
        {
            List<Area> areas = new List<Area>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Area area = new Area();
                    area.Id = Convert.ToInt32(reader["Id"].ToString());
                    area.LayoutId = Convert.ToInt32(reader["LayoutId"].ToString());
                    area.Description = reader["Description"].ToString();
                    area.CoordX = Convert.ToInt32(reader["CoordX"].ToString());
                    area.CoordY = Convert.ToInt32(reader["CoordY"].ToString());
                    areas.Add(area);
                }
            }

            return areas;
        }
    }
}
