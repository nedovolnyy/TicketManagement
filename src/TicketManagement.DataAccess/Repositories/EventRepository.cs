using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventRepository : BaseRepository<Event>, IEventRepository
    {
        private readonly IDatabaseContext _databaseContext;

        internal EventRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        protected override void AddParamsForInsert(Event entity, SqlCommand cmd)
        {
            cmd.CommandText = "spEventInsert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@EventTime", entity.EventTime);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
            cmd.Parameters.AddWithValue("@EventEndTime", entity.EventEndTime);
        }

        protected override void AddParamsForUpdate(Event entity, SqlCommand cmd)
        {
            cmd.CommandText = "spEventUpdate";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@EventTime", entity.EventTime);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@LayoutId", entity.LayoutId);
            cmd.Parameters.AddWithValue("@EventEndTime", entity.EventEndTime);
        }

        protected override void AddParamsForDelete(int id, SqlCommand cmd)
        {
            cmd.CommandText = "spEventDelete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, SqlCommand cmd)
        {
            cmd.CommandText = "spEventGetById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
        }

        protected override void GetAllCommandParameters(SqlCommand cmd)
        {
            cmd.CommandText = "spEventGetAll";
            cmd.CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Method for populate data by layoutId.
        /// </summary>
        /// <param name="layoutId">layoutId.</param>
        /// <returns><see cref="Event"/>List&lt;Event&gt;.</returns>
        public IEnumerable<Event> GetAllByLayoutId(int layoutId)
        {
            var cmd = _databaseContext.Connection.CreateCommand();
            cmd.CommandText = "spEventForValidationByLayout";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LayoutId", layoutId);
            using var reader = cmd.ExecuteReader();
            return Maps(reader);
        }

        /// <summary>
        /// Method for count empty seats.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns><see cref="int"/>Count empty seats.</returns>
        public int GetCountEmptySeats(int id)
        {
            var cmd = _databaseContext.Connection.CreateCommand();
            cmd.CommandText = "spEventCountEmptySeats";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            var count = cmd.ExecuteScalar();
            if (count is null)
            {
                return default;
            }

            return int.Parse(count.ToString());
        }

        /// <summary>
        /// Method for validation data by seats in Area.
        /// </summary>
        /// <param name="layoutId">layoutId.</param>
        /// <returns><see cref="int"/>Count empty seats.</returns>
        public int GetCountSeats(int layoutId)
        {
            var cmd = _databaseContext.Connection.CreateCommand();
            cmd.CommandText = "spEventCountSeats";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LayoutId", layoutId);
            var count = cmd.ExecuteScalar();
            if (count is null)
            {
                return default;
            }

            return int.Parse(count.ToString());
        }

        protected override Event Map(SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                reader.Read();
                return new Event(id: int.Parse(reader["Id"].ToString()),
                                     name: reader["Name"].ToString(),
                                     eventTime: DateTimeOffset.Parse(reader["EventTime"].ToString()),
                                     description: reader["Description"].ToString(),
                                     layoutId: int.Parse(reader["LayoutId"].ToString()),
                                     eventEndTime: DateTime.Parse(reader["EventEndTime"].ToString()));
            }

            return null;
        }

        protected override List<Event> Maps(SqlDataReader reader)
        {
            var evnts = new List<Event>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var evnt = new Event(id: int.Parse(reader["Id"].ToString()),
                                     name: reader["Name"].ToString(),
                                     eventTime: DateTimeOffset.Parse(reader["EventTime"].ToString()),
                                     description: reader["Description"].ToString(),
                                     layoutId: int.Parse(reader["LayoutId"].ToString()),
                                     eventEndTime: DateTime.Parse(reader["EventEndTime"].ToString()));
                    evnts.Add(evnt);
                }
            }

            return evnts;
        }
    }
}
