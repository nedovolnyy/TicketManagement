﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.ADO;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class LayoutRepository : BaseRepository<Layout>, ILayoutRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public LayoutRepository()
        {
            _databaseContext = new DatabaseContext();
        }

        internal LayoutRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        protected override string ActionToSqlString(string action) => action switch
        {
            "Insert" => "INSERT INTO Layout (Name, VenueId, Description) VALUES (@Name, @VenueId, @Description);" +
                            "SELECT CAST (SCOPE_IDENTITY() AS INT)",
            "Update" => "UPDATE Layout SET Name = @Name, VenueId = @VenueId, Description = @Description Where Id = @Id",
            "Delete" => "DELETE FROM Layout WHERE Id = @Id",
            "GetById" => "SELECT Id, Name, VenueId, Description FROM Layout WHERE Id = @Id",
            "GetAll" => "SELECT Id, Name, VenueId, Description FROM Layout",
            "ActionForValidate" => "SELECT Id, Name, VenueId, Description FROM Layout WHERE VenueId = @VenueId",
            _ => ""
        };

        protected override void InsertCommandParameters(Layout entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@VenueId", entity.VenueId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
        }

        protected override void UpdateCommandParameters(Layout entity, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@VenueId", entity.VenueId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
        }

        protected override void DeleteCommandParameters(int id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetByIdCommandParameters(int id, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Id", id);
        }

        /// <summary>
        /// Base Method for Populate Data by key.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>Get all Entity by VenueId.</returns>
        public IEnumerable<Layout> GetAllByVenueId(int id)
        {
            try
            {
                using (SqlConnection sqlConnection = _databaseContext.Connection)
                {
                    using (var cmd = sqlConnection.CreateCommand())
                    {
                        cmd.CommandText = ActionToSqlString("ActionForValidate");
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@VenueId", id);
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

        protected override Layout Map(SqlDataReader reader)
        {
            Layout layout = new Layout();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    layout = new Layout(id: int.Parse(reader["Id"].ToString()),
                                        name: reader["Name"].ToString(),
                                        venueId: int.Parse(reader["VenueId"].ToString()),
                                        description: reader["Description"].ToString());
                }
            }
            else
            {
                throw new ValidationException("Don't have layouts to show!", "");
            }

            return layout;
        }

        protected override List<Layout> Maps(SqlDataReader reader)
        {
            List<Layout> areas = new List<Layout>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Layout layout = new Layout(id: int.Parse(reader["Id"].ToString()),
                                               name: reader["Name"].ToString(),
                                               venueId: int.Parse(reader["VenueId"].ToString()),
                                               description: reader["Description"].ToString());
                    areas.Add(layout);
                }
            }
            else
            {
                throw new ValidationException("Don't have layouts to show!", "");
            }

            return areas;
        }

        protected override void GetAllCommandParameters(SqlCommand cmd)
        {
        }
    }
}
