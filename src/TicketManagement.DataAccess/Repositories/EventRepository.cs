﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public override async Task<int> InsertAsync(IEvent evnt)
        {
            var paramName = new SqlParameter("@Name", evnt.Name);
            var paramEventTime = new SqlParameter("@EventTime", evnt.EventTime);
            var paramDescription = new SqlParameter("@Description", evnt.Description);
            var paramLayoutId = new SqlParameter("@LayoutId", evnt.LayoutId);
            var paramEventEndTime = new SqlParameter("@EventEndTime", evnt.EventEndTime);
            var paramEventLogoImage = new SqlParameter("@EventLogoImage", evnt.EventLogoImage);
            return await _databaseContext.Instance.Database
                .ExecuteSqlRawAsync("spEventInsert @Name, @EventTime, @Description, @LayoutId, @EventEndTime, @EventLogoImage, @Price",
                                               paramName, paramEventTime, paramDescription, paramLayoutId, paramEventEndTime, paramEventLogoImage, decimal.Zero);
        }

        public async Task<int> InsertAsync(IEvent evnt, decimal price)
        {
            var paramName = new SqlParameter("@Name", evnt.Name);
            var paramEventTime = new SqlParameter("@EventTime", evnt.EventTime);
            var paramDescription = new SqlParameter("@Description", evnt.Description);
            var paramLayoutId = new SqlParameter("@LayoutId", evnt.LayoutId);
            var paramEventEndTime = new SqlParameter("@EventEndTime", evnt.EventEndTime);
            var paramEventLogoImage = new SqlParameter("@EventLogoImage", evnt.EventLogoImage);
            var paramPrice = new SqlParameter("@Price", price);
            return await _databaseContext.Instance.Database
                .ExecuteSqlRawAsync("spEventInsert @Name, @EventTime, @Description, @LayoutId, @EventEndTime, @EventLogoImage, @Price",
                                               paramName, paramEventTime, paramDescription, paramLayoutId, paramEventEndTime, paramEventLogoImage, paramPrice);
        }

        public new async Task UpdateAsync(IEvent evnt)
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

        public override async Task<int> DeleteAsync(int id)
        {
            var paramId = new SqlParameter("@Id", id);
            return await _databaseContext.Instance.Database
                .ExecuteSqlRawAsync("spEventDelete @Id", paramId);
        }

        public override async Task<IEvent> GetByIdAsync(int id)
        {
            var paramId = new SqlParameter("@Id", id);
            return (await _dbSet.FromSqlRaw("spEventGetById @Id", paramId).ToListAsync())[0];
        }

        public override IQueryable<IEvent> GetAll()
            => _dbSet.FromSqlRaw("spEventGetAll").AsNoTracking();

        public IQueryable<IEvent> GetAllByLayoutId(int layoutId)
        {
            var paramLayoutId = new SqlParameter("@LayoutId", layoutId);
            return _dbSet.FromSqlRaw("spEventForValidationByLayout @LayoutId", paramLayoutId).AsNoTracking();
        }

        public async Task<int> GetSeatsAvailableCountAsync(int id)
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
