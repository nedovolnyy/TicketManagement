using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    public class VenueRepository : BaseRepository<Venue>, IRepository<Venue>
    {
        public VenueRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Passes the parameters for Insert Statement.
        /// </summary>
        /// <param name="entity">.</param>
        /// <param name="cmd">..</param>
        protected override void InsertCommandParameters(Venue entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@Address", entity.Address);
            cmd.Parameters.AddWithValue("@Phone", entity.Phone);
        }

        /// <summary>
        /// Passes the parameters for Update Statement.
        /// </summary>
        /// <param name="entity">.</param>
        /// <param name="cmd">..</param>
        protected override void UpdateCommandParameters(Venue entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@Address", entity.Address);
            cmd.Parameters.AddWithValue("@Phone", entity.Phone);
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
        protected override Venue Map(SqlDataReader reader)
        {
            Venue venue = new Venue();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    venue.Id = Guid.Parse(reader["Id"].ToString());
                    venue.Description = reader["Description"].ToString();
                    venue.Address = reader["Address"].ToString();
                    venue.Phone = reader["Phone"].ToString();
                }
            }

            return venue;
        }

        /// <summary>
        /// Maps data for populate all statement.
        /// </summary>
        /// <param name="reader">.</param>
        /// <returns>..</returns>
        protected override List<Venue> Maps(SqlDataReader reader)
        {
            List<Venue> venues = new List<Venue>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Venue venue = new Venue();
                    venue.Id = Guid.Parse(reader["Id"].ToString());
                    venue.Description = reader["Description"].ToString();
                    venue.Address = reader["Address"].ToString();
                    venue.Phone = reader["Phone"].ToString();
                    venues.Add(venue);
                }
            }

            return venues;
        }
    }
}
