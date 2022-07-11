using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class EventRepository : BaseRepository<Event>, IEventRepository
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<Event> _dbSet;

        public EventRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Events;
        }

        public override async Task InsertAsync(Event evnt)
            => await InsertAsync(evnt, decimal.Zero);

        public async Task InsertAsync(Event evnt, decimal price)
        {
            var paramName = new SqlParameter("@Name", evnt.Name);
            var paramEventTime = new SqlParameter("@EventTime", evnt.EventTime);
            var paramDescription = new SqlParameter("@Description", evnt.Description);
            var paramLayoutId = new SqlParameter("@LayoutId", evnt.LayoutId);
            var paramEventEndTime = new SqlParameter("@EventEndTime", evnt.EventEndTime);
            var paramEventLogoImage = new SqlParameter("@EventLogoImage", evnt.EventLogoImage);
            var paramPrice = new SqlParameter("@Price", price);
            await _databaseContext.Instance.Database
                .ExecuteSqlRawAsync("spEventInsert @Name, @EventTime, @Description, @LayoutId, @EventEndTime, @EventLogoImage, @Price",
                                               paramName, paramEventTime, paramDescription, paramLayoutId, paramEventEndTime, paramEventLogoImage, paramPrice);
        }

        public override async Task UpdateAsync(Event evnt)
        {
            var paramId = new SqlParameter("@Id", evnt.Id);
            var paramName = new SqlParameter("@Name", evnt.Name);
            var paramEventTime = new SqlParameter("@EventTime", evnt.EventTime);
            var paramDescription = new SqlParameter("@Description", evnt.Description);
            var paramLayoutId = new SqlParameter("@LayoutId", evnt.LayoutId);
            var paramEventEndTime = new SqlParameter("@EventEndTime", evnt.EventEndTime);
            var paramEventLogoImage = new SqlParameter("@EventLogoImage", evnt.EventLogoImage);
            await _databaseContext.Instance.Database
                .ExecuteSqlRawAsync("spEventUpdate @Id, @Name, @EventTime, @Description, @LayoutId, @EventEndTime, @EventLogoImage",
                    paramId, paramName, paramEventTime, paramDescription, paramLayoutId, paramEventEndTime, paramEventLogoImage);
        }

        public async Task UpdateAsync(Event evnt, decimal price)
        {
            var paramId = new SqlParameter("@Id", evnt.Id);
            var paramName = new SqlParameter("@Name", evnt.Name);
            var paramEventTime = new SqlParameter("@EventTime", evnt.EventTime);
            var paramDescription = new SqlParameter("@Description", evnt.Description);
            var paramLayoutId = new SqlParameter("@LayoutId", evnt.LayoutId);
            var paramEventEndTime = new SqlParameter("@EventEndTime", evnt.EventEndTime);
            var paramEventLogoImage = new SqlParameter("@EventLogoImage", evnt.EventLogoImage);
            var paramPrice = new SqlParameter("@Price", price);
            await _databaseContext.Instance.Database
                .ExecuteSqlRawAsync("spEventUpdate @Id, @Name, @EventTime, @Description, @LayoutId, @EventEndTime, @EventLogoImage, @Price",
                    paramId, paramName, paramEventTime, paramDescription, paramLayoutId, paramEventEndTime, paramEventLogoImage, paramPrice);
        }

        public new async Task DeleteAsync(int id)
        {
            var paramId = new SqlParameter("@Id", id);
            await _databaseContext.Instance.Database
                .ExecuteSqlRawAsync("spEventDelete @Id", paramId);
        }

        public override async Task<Event> GetByIdAsync(int id)
        {
            var paramId = new SqlParameter("@Id", id);
            return (await _dbSet.FromSqlRaw("spEventGetById @Id", paramId).ToListAsyncSafe())[0];
        }

        public override IQueryable<Event> GetAll()
            => _dbSet.FromSqlRaw("spEventGetAll").AsNoTracking().IgnoreQueryFilters();

        public IQueryable<Event> GetAllByLayoutId(int layoutId)
        {
            var paramLayoutId = new SqlParameter("@LayoutId", layoutId);
            return _dbSet.FromSqlRaw("spEventForValidationByLayout @LayoutId", paramLayoutId).AsNoTracking().IgnoreQueryFilters();
        }

        public async Task<bool> IsAllAvailableSeatsAsync(int id)
        {
            var paramId = new SqlParameter("@Id", id);
            var paramIsAllAvailableSeats = new SqlParameter
            {
                ParameterName = "@IsAllAvailableSeats",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Output,
            };

            await _databaseContext.Instance.Database
                .ExecuteSqlRawAsync("spEventIsAllAvailableSeats @Id, @IsAllAvailableSeats OUT", paramId, paramIsAllAvailableSeats);
            return (bool)paramIsAllAvailableSeats.Value;
        }

        public async Task<int> GetSeatsAvailableCountAsync(int id)
        {
            var paramId = new SqlParameter("@Id", id);
            var paramCountAvailableSeats = new SqlParameter
            {
                ParameterName = "@CountAvailableSeats",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
            };

            await _databaseContext.Instance.Database
                .ExecuteSqlRawAsync("spEventCountAvailableSeats @Id, @CountAvailableSeats OUT", paramId, paramCountAvailableSeats);
            return (int)paramCountAvailableSeats.Value;
        }

        public async Task<int> GetSeatsCountAsync(int layoutId)
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
