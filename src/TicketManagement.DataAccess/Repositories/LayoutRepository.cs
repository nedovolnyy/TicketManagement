using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    public class LayoutRepository : BaseRepository<Layout>, ILayoutRepository
    {
        public LayoutRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Passes the parameters for Insert Statement.
        /// </summary>
        /// <param name="entity">.</param>
        /// <param name="cmd">..</param>
        protected override void InsertCommandParameters(Layout entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@VenueId", entity.VenueId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
        }

        /// <summary>
        /// Passes the parameters for Update Statement.
        /// </summary>
        /// <param name="entity">.</param>
        /// <param name="cmd">..</param>
        protected override void UpdateCommandParameters(Layout entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@VenueId", entity.VenueId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
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
        protected override Layout Map(SqlDataReader reader)
        {
            Layout layout = new Layout();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    layout.Id = Convert.ToInt32(reader["Id"].ToString());
                    layout.VenueId = Convert.ToInt32(reader["VenueId"].ToString());
                    layout.Description = reader["Description"].ToString();
                }
            }

            return layout;
        }

        /// <summary>
        /// Maps data for populate all statement.
        /// </summary>
        /// <param name="reader">.</param>
        /// <returns>..</returns>
        protected override List<Layout> Maps(SqlDataReader reader)
        {
            List<Layout> areas = new List<Layout>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Layout layout = new Layout();
                    layout.Id = Convert.ToInt32(reader["Id"].ToString());
                    layout.VenueId = Convert.ToInt32(reader["VenueId"].ToString());
                    layout.Description = reader["Description"].ToString();
                    areas.Add(layout);
                }
            }

            return areas;
        }
    }
}
