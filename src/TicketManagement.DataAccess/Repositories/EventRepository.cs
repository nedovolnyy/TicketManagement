using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventRepository : BaseRepository<IEvent>, IEventRepository
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<Event> _dbSet;

        public EventRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Events;
        }

        public override async Task<int> Insert(IEvent evnt)
        {
            var paramName = new SqlParameter("@Name", evnt.Name);
            var paramEventTime = new SqlParameter("@EventTime", evnt.EventTime);
            var paramDescription = new SqlParameter("@Description", evnt.Description);
            var paramLayoutId = new SqlParameter("@LayoutId", evnt.LayoutId);
            var paramEventEndTime = new SqlParameter("@EventEndTime", evnt.EventEndTime);
            return await _databaseContext.Instance.Database
                .ExecuteSqlRawAsync("spEventInsert @Name, @EventTime, @Description, @LayoutId, @EventEndTime", paramName, paramEventTime, paramDescription, paramLayoutId, paramEventEndTime);
        }

        public new async Task Update(IEvent evnt)
        {
            var paramId = new SqlParameter("@Id", evnt.Id);
            var paramName = new SqlParameter("@Name", evnt.Name);
            var paramEventTime = new SqlParameter("@EventTime", evnt.EventTime);
            var paramDescription = new SqlParameter("@Description", evnt.Description);
            var paramLayoutId = new SqlParameter("@LayoutId", evnt.LayoutId);
            var paramEventEndTime = new SqlParameter("@EventEndTime", evnt.EventEndTime);
            await _databaseContext.Instance.Database
                .ExecuteSqlRawAsync("spEventUpdate @Id, @Name, @EventTime, @Description, @LayoutId, @EventEndTime",
                    paramId, paramName, paramEventTime, paramDescription, paramLayoutId, paramEventEndTime);
        }

        public override async Task<int> Delete(int id)
        {
            var paramId = new SqlParameter("@Id", id);
            return await _databaseContext.Instance.Database
                .ExecuteSqlRawAsync("spEventDelete @Id", paramId);
        }

        public override async Task<IEvent> GetById(int id)
        {
            var paramId = new SqlParameter("@Id", id);
            return (await _dbSet.FromSqlRaw("spEventGetById @Id", paramId).ToListAsync())[0];
        }

        public override async Task<IEnumerable<IEvent>> GetAll()
        {
            return await _dbSet.FromSqlRaw("spEventGetAll").ToListAsync();
        }

        public async Task<IEnumerable<IEvent>> GetAllByLayoutId(int layoutId)
        {
            var paramLayoutId = new SqlParameter("@LayoutId", layoutId);
            return await _dbSet.FromSqlRaw("spEventForValidationByLayout @LayoutId", paramLayoutId).ToListAsync();
        }

        public async Task<int> GetSeatsAvailableCount(int id)
        {
            var paramId = new SqlParameter("@Id", id);
            var paramCountEmptySeats = new SqlParameter
            {
                ParameterName = "@CountEmptySeats",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
            };

            await _databaseContext.Instance.Database
                .ExecuteSqlRawAsync("spEventCountEmptySeats @Id, @CountEmptySeats OUT", paramId, paramCountEmptySeats);
            return (int)paramCountEmptySeats.Value;
        }

        public async Task<int> GetSeatsCount(int layoutId)
        {
            var paramLayoutId = new SqlParameter("@LayoutId", layoutId);
            var paramCountSeats = new SqlParameter
            {
                ParameterName = "@CountSeats",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
            };

            await _databaseContext.Instance.Database
                .ExecuteSqlRawAsync("spEventCountSeats @LayoutId, @CountSeats OUT", paramLayoutId, paramCountSeats);
            return (int)paramCountSeats.Value;
        }
    }
}
