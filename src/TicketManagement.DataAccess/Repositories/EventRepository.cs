using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        protected override void AddParamsForInsert(Event entity, DbCommand cmd)
        {
            cmd.CommandText = "spEventInsert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.AddWithValue("@Name", entity.Name);
            cmd.AddWithValue("@EventTime", entity.EventTime);
            cmd.AddWithValue("@Description", entity.Description);
            cmd.AddWithValue("@LayoutId", entity.LayoutId);
            cmd.AddWithValue("@EventEndTime", entity.EventEndTime);
        }

        protected override void AddParamsForUpdate(Event entity, DbCommand cmd)
        {
            cmd.CommandText = "spEventUpdate";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.AddWithValue("@Id", entity.Id);
            cmd.AddWithValue("@Name", entity.Name);
            cmd.AddWithValue("@EventTime", entity.EventTime);
            cmd.AddWithValue("@Description", entity.Description);
            cmd.AddWithValue("@LayoutId", entity.LayoutId);
            cmd.AddWithValue("@EventEndTime", entity.EventEndTime);
        }

        protected override void AddParamsForDelete(int id, DbCommand cmd)
        {
            cmd.CommandText = "spEventDelete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.AddWithValue("@Id", id);
        }

        protected override void AddParamsForGetById(int id, DbCommand cmd)
        {
            cmd.CommandText = "spEventGetById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.AddWithValue("@Id", id);
        }

        protected override void GetAllCommandParameters(DbCommand cmd)
        {
            cmd.CommandText = "spEventGetAll";
            cmd.CommandType = CommandType.StoredProcedure;
        }

        public IEnumerable<Event> GetAllByLayoutId(int layoutId)
        {
            var cmd = _databaseContext.Connection.CreateCommand();
            cmd.CommandText = "spEventForValidationByLayout";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.AddWithValue("@LayoutId", layoutId);
            using var reader = cmd.ExecuteReader();
            return Maps(reader);
        }

        public int GetCountEmptySeats(int id)
        {
            var cmd = _databaseContext.Connection.CreateCommand();
            cmd.CommandText = "spEventCountEmptySeats";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.AddWithValue("@Id", id);
            var count = cmd.ExecuteScalar();
            if (count is null)
            {
                return default;
            }

            return int.Parse(count.ToString());
        }

        public int GetCountSeats(int layoutId)
        {
            var cmd = _databaseContext.Connection.CreateCommand();
            cmd.CommandText = "spEventCountSeats";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.AddWithValue("@LayoutId", layoutId);
            var count = cmd.ExecuteScalar();
            if (count is null)
            {
                return default;
            }

            return int.Parse(count.ToString());
        }

        protected override Event Map(DbDataReader reader)
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

        protected override List<Event> Maps(DbDataReader reader)
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
